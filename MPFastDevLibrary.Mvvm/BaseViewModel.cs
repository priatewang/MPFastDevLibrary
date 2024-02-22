using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MPFastDevLibrary.Mvvm
{
    public class BaseViewModel : ObservableObject
    {
        public static string UINameSapce = "";

        public static void SetUINameSapce(string value)
        {
            UINameSapce = value;
        }

        private string UIElementName = "";
        private string UIElementType = "";

        /// <summary>
        /// 当前ViewModel对应的元素（窗体/页面/控件）
        /// </summary>
        public FrameworkElement UIElement { get; set; }

        /// <summary>
        /// 主窗体
        /// </summary>
        public Window WindowMain { get; set; }

        /// <summary>
        /// 窗体/页面/控件的关闭委托
        /// </summary>
        public EventHandler CloseCallBack = null;

        public BaseViewModel()
        {
            WindowMain = Application.Current.MainWindow;
            SetUIElement();
            UIElement.DataContext = this;
        }

        #region 通过反射创建对应的UI元素
        public void SetUIElement()
        {
            Type childType = this.GetType(); //获取子类的类型
            string name = this.GetType().Name;
            UIElementName = name.Replace("_VM", "");
            UIElementName = UIElementName.Replace("`1", ""); //应对泛型实体

            if (name.Contains("Window"))
            {
                UIElementType = "Windows";
                UIElement = GetElement<Window>();
                (UIElement as Window).Closing += (s, e) =>
                {
                    CloseCallBack?.Invoke(s, e);
                };
            }
            else if (name.Contains("Page"))
            {
                UIElementType = "Pages";
                UIElement = GetElement<Page>();
                (UIElement as Page).Unloaded += (s, e) =>
                {
                    CloseCallBack?.Invoke(s, e);
                };
            }
            else if (name.Contains("UC"))
            {
                UIElementType = "UserControls";
                UIElement = GetElement<UserControl>();
                (UIElement as UserControl).Unloaded += (s, e) =>
                {
                    CloseCallBack?.Invoke(s, e);
                };
            }
            else
            {
                throw new Exception("元素名不规范");
            }
        }

        public E GetElement<E>()
        {
            Type type = GetFormType(UINameSapce + "." + UIElementType + "." + UIElementName);
            E element = (E)Activator.CreateInstance(type);
            return element;
        }

        public static Type GetFormType(string fullName)
        {
            Assembly assembly = Assembly.Load(UINameSapce);
            Type type = assembly.GetType(fullName, true, false);
            return type;
        }
        #endregion

        #region 窗体操作
        public void Show()
        {
            if (UIElement is Window)
            {
                (UIElement as Window).Show();
            }
            else
            {
                throw new Exception("元素类型不正确");
            }
        }

        public void ShowDialog()
        {
            if (UIElement is Window)
            {
                (UIElement as Window).ShowDialog();
            }
            else
            {
                throw new Exception("元素类型不正确");
            }
        }

        public void Close()
        {
            if (UIElement is Window)
            {
                (UIElement as Window).Close();
            }
            else
            {
                throw new Exception("元素类型不正确");
            }
        }

        public void Hide()
        {
            if (UIElement is Window)
            {
                (UIElement as Window).Hide();
            }
            else
            {
                throw new Exception("元素类型不正确");
            }
        }
        #endregion

        public virtual void Stop() { }

        public virtual void Update() { }
    }
}
