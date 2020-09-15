using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DnDBlazorReference.Client.Data
{
    public class EditAndStyleController
    {
        public delegate void StateChangedDelegate();

        private bool _isEditting;
        public bool IsEditting 
        {
            get => _isEditting;
            set
            {
                bool diff = _isEditting != value;
                _isEditting = value;
                if (diff)
                {
                    EdittingStateChanged?.Invoke();
                }
            }
        }
        public event StateChangedDelegate EdittingStateChanged;

        public EditAndStyleController()
        {
            _isEditting = true;
        }
    }
}
