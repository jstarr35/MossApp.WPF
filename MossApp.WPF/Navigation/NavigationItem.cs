using MossApp.Utilities.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MossApp.WPF.Navigation
{
    public class NavigationItem : BindableBase
    {
        private string m_label;
        private Func<object> m_action;
        private bool m_isSelected;

        public string Label
        {
            get
            {
                return m_label;
            }

            set
            {
             

                SetProperty(ref m_label, value);
            }
        }

        public Func<object> Action
        {
            get
            {
                return m_action;
            }

            set
            {
               

                SetProperty(ref m_action, value);
            }
        }

        public bool IsSelected
        {
            get
            {
                return m_isSelected;
            }

            set
            {
               

                SetProperty(ref m_isSelected, value);
            }
        }

        public NavigationItem()
        {
            m_label = null;
            m_action = null;
            m_isSelected = false;
        }
    }
}
