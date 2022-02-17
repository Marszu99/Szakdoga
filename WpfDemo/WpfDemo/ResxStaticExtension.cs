using System;
using System.Linq;
using System.Windows;
using System.Windows.Markup;
using TimeSheet.Resource;

namespace WpfDemo
{
    public class ResxStaticExtension : StaticExtension
    {
        DependencyProperty _targetProperty;
        object _targetObject;
        readonly string resId;

        public ResxStaticExtension(string member) : base(member)
        {
            resId = member.Split('.').Last();
            LanguageChanged += (o, e) => _targetObject.GetType().GetProperty(_targetProperty.Name).SetValue(_targetObject, ResourceHandler.GetResourceString(resId));
        }

        static event EventHandler LanguageChanged;

        public static void OnLanguageChanged()
        {
            LanguageChanged?.Invoke(null, null);
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            var service = serviceProvider.GetService(typeof(IProvideValueTarget)) as IProvideValueTarget;
            _targetProperty = service.TargetProperty as DependencyProperty;
            _targetObject = service.TargetObject;

            return base.ProvideValue(serviceProvider);
        }

    }
}
