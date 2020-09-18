using DnDBlazorReference.Client.Data;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DnDBlazorReference.Client.Shared
{
    public class ServiceRedrawReactiveComponent : ComponentBase, IDisposable
    {
        [Inject]
        public DataStorage DataStore { get; set; }
        [Inject]
        public EditAndStyleController EditAndStyle { get; set; }


        protected override void OnInitialized()
        {
            base.OnInitialized();

            EditAndStyle.EdittingStateChanged += EditAndStyle_EdittingStateChanged;
            DataStore.StorageStateChanged += DataStore_StorageStateChanged;
        }

        protected virtual void DataStore_StorageStateChanged()
        {
            StateHasChanged();
        }

        protected virtual void EditAndStyle_EdittingStateChanged()
        {
            StateHasChanged();
        }

        public virtual void Dispose()
        {
            EditAndStyle.EdittingStateChanged -= EditAndStyle_EdittingStateChanged;
            DataStore.StorageStateChanged -= DataStore_StorageStateChanged;
        }
    }
}
