﻿using System;
using System.Collections.ObjectModel;
using Danfoss.CaseControllerApp.Core.Services.Bluetooth.Abstract;

namespace Danfoss.CaseControllerApp.Core.Services.Bluetooth.Common
{
    public static class SyncFacade
    {
        public static EventBatch SyncTo<TModel, TViewModel>(this ISyncable<TModel> syncable, ObservableCollection<TViewModel> target, Func<TModel, TViewModel> convert)
        {
            foreach (var scanResult in syncable.Items)
            {
                target.Add(convert(scanResult));
            }

            var add = syncable.ItemAdded.Subscribe(device =>
            {
                target.Add(convert(device));
            });

            var clear = syncable.Cleared.Subscribe(x => target.Clear());

            return new EventBatch(add, clear);
        }
    }
}