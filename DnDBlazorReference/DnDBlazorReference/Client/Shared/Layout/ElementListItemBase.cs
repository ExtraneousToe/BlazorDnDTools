using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DnDBlazorReference.Client.Shared.Layout
{
    public class ElementListItemBase : ComponentBase
    {
        [CascadingParameter(Name = "ParentList")]
        public ElementList ParentList { get; set; }

        public string ActiveCssClass => "active";

        public virtual void SelectItem()
        {
            ParentList.SelectItem(this);
        }
    }

    public class ElementListItemBase<T> : ElementListItemBase where T : class
    {
        [Parameter]
        public T ListItem { get; set; }

        [Parameter]
        public EventCallback<T> OnSelectedItem { get; set; }

        public string GetCssByItem(T item)
        {
            return item == ListItem ? ActiveCssClass : string.Empty;
        }

        public override void SelectItem()
        {
            base.SelectItem();
            OnSelectedItem.InvokeAsync(ListItem);
        }
    }
}
