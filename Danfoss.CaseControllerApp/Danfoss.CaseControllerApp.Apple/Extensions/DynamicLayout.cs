using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Cirrious.FluentLayouts.Touch;
using Danfoss.CaseControllerApp.Apple.Exceptions;
using Daven.SyntaxExtensions;
using Foundation;
using UIKit;

namespace Danfoss.CaseControllerApp.Apple.Extensions
{
    public class DynamicLayout : NSObject
    {
        private readonly UIView _parent;
        private readonly DynamicSection[] _sections;
        private NSLayoutConstraint[] _oldConstraints;
        private bool _animated = false;
        private SemaphoreSlim _semaphoreSlim;

        private SemaphoreSlim SemaphoreSlim
        {
            get { return _semaphoreSlim = _semaphoreSlim ?? new SemaphoreSlim(1); }
        }

        private const double AnimationDuration = 0.5d;

        public DynamicLayout(UIView parent, DynamicSection[] sections)
        {
            _parent = parent;
            _sections = sections;

            Validate();

            Refresh();
        }

        private void Validate()
        {
            foreach (var dynamicSection in _sections)
            {
                var targets = ExtractTargets(dynamicSection);
                if (targets == null) continue;

                foreach (var target in targets)
                {
                    if (target.Superview == null)
                    {
                        throw new DynamicLayoutException("Every view in dynamic layout on the initialization step must be child of superview. " +
                                                         "In other words it should be part of the visual tree.");
                    }

                    if (target.TranslatesAutoresizingMaskIntoConstraints)
                    {
                        throw new DynamicLayoutException("Every view in dynamic layout should have property TranslateAutoresizingMaskIntoConstraints setted into false.");
                    }
                }
            }
        }

        /// <summary>
        /// If refresh performs without animation, the method does not create any tasks, so the synchronization such as await/async is not necessary.
        /// </summary>
        public async Task Refresh(bool animated = false, bool withInternalLock = false)
        {
            _animated = animated;

            if (withInternalLock)
            {
                await SemaphoreSlim.Lock(RefreshInternal);
            }
            else
            {
                await RefreshInternal();
            }

            _animated = false;
        }

        private async Task RefreshInternal()
        {
            UIView previousTarget = null;
            var constraints = new List<FluentLayout>();
            foreach (var dynamicSection in _sections)
            {
                AddOrRemoveChilds(dynamicSection);

                AddConstraints(constraints, dynamicSection, ref previousTarget);
            }

            ApplyWhenLastCallback(constraints);
            await ApplyConstraints(constraints);
            AnimateAdd();
        }

        private void AddConstraints(List<FluentLayout> constraints, DynamicSection dynamicSection, ref UIView previousTarget)
        {
            if (dynamicSection.isApplicable())
            {
                if (dynamicSection.getConstraints != null)
                {
                    constraints.AddRange(dynamicSection.getConstraints());
                }
                if (dynamicSection.getConstraintsWithDependent != null)
                {
                    constraints.AddRange(dynamicSection.getConstraintsWithDependent(previousTarget ?? _parent));
                }
                previousTarget = dynamicSection.sectionTarget;
            }
        }

        private void ApplyWhenLastCallback(List<FluentLayout> constraints)
        {
            var lastApplicable = _sections.LastOrDefault(x => x.isApplicable());
            if (lastApplicable != null)
            {
                constraints.AddRange(lastApplicable.whenLast.Call().SelfOrEmpty());
            }
        }

        private Task ApplyConstraints(List<FluentLayout> constraints)
        {
            if (constraints.Any())
            {
                if (_animated && _startRemoveAnimationCount > 0)
                {
                    return Task.Run(() =>
                    {
                        while (_startRemoveAnimationCount > 0) ;

                        InvokeOnMainThread(() => RemoveOldConstraintsAndAddNew(constraints));
                    });
                }
                else
                {
                    RemoveOldConstraintsAndAddNew(constraints);
                }
            }
            else
            {
                _oldConstraints = null;
            }

            return TaskHelper.Complete();
        }

        private void AnimateAdd()
        {
            if (_animated == false) return;

            foreach (var s in _parent.Subviews)
            {
                if (s.Alpha == 0)
                {
                    UIView.Animate(AnimationDuration,
                    () =>
                    {
                        s.Alpha = 1;
                    },
                    () =>
                    {
                        s.Alpha = 1;
                    });
                }
            }
        }

        private void RemoveOldConstraintsAndAddNew(List<FluentLayout> constraints)
        {
            if (_oldConstraints != null) _parent.RemoveConstraints(_oldConstraints);
            _oldConstraints = constraints.ToNative();
            _parent.AddConstraints(_oldConstraints);
            _parent.SetNeedsUpdateConstraints();
        }

        private void AddOrRemoveChilds(DynamicSection dynamicSection)
        {
            var targets = ExtractTargets(dynamicSection);

            if (targets == null) return;

            foreach (var target in targets)
            {
                if (dynamicSection.isApplicable())
                {
                    if (target.Superview == null)
                    {
                        AddToParent(target);

                        dynamicSection.onShown.Call();
                    }
                }
                else
                {
                    if (target.Superview != null)
                    {
                        RemoveFromParent(target);
                    }
                }
            }
        }

        private IEnumerable<UIView> ExtractTargets(DynamicSection dynamicSection)
        {
            return dynamicSection.targets ?? (dynamicSection.sectionTarget != null ? new UIView[] { dynamicSection.sectionTarget } : null);
        }

        private void AddToParent(UIView target)
        {
            if (_animated)
            {
                target.Alpha = 0;
                _parent.Add(target);
                
            }
            else
            {
                target.Alpha = 1;
                _parent.Add(target);
            }
        }

        private int _startRemoveAnimationCount = 0;
        private void RemoveFromParent(UIView target)
        {
            if (_animated)
            {
                _startRemoveAnimationCount++;
                UIView.Animate(AnimationDuration,
                    () => target.Alpha = 0,
                    () =>
                    {
                        _startRemoveAnimationCount--;
                        target.RemoveFromSuperview();
                    });
            }
            else
            {
                target.RemoveFromSuperview();
            }
        }
    }
}