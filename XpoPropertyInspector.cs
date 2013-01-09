using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Drawing;
//using System.Linq;
using System.Windows.Forms;
using DevExpress.CodeRush.Core;
using DevExpress.CodeRush.PlugInCore;
using DevExpress.CodeRush.StructuralParser;

namespace CodeIssueSearcher
{
    public partial class CodeIssueSearcher : StandardPlugIn
    {
        // DXCore-generated code...
        #region InitializePlugIn
        public override void InitializePlugIn()
        {
            base.InitializePlugIn();

            //
            // TODO: Add your initialization code here.
            //
        }
        #endregion
        #region FinalizePlugIn
        public override void FinalizePlugIn()
        {
            //
            // TODO: Add your finalization code here.
            //

            base.FinalizePlugIn();
        }
        #endregion

        private void my_issue_provider_CheckCodeIssues(object sender, CheckCodeIssuesEventArgs ea)
        {
            try
            {
                PropertyFilter filter = ElementFilters.Property;
                IEnumerable<IElement> enumerable = ea.GetEnumerable(ea.Scope, filter);

                foreach (IElement element in enumerable)
                {
                    IPropertyElement propertyElement = element as IPropertyElement;
                    if (propertyElement == null)
                        continue;

                    bool set_prop_found = false;

                    if (propertyElement.SetMethod != null)
                    {
                        set_prop_found = ContainsSetProperty(element.Name, propertyElement.SetMethod.AllChildren, ea);
                    }

                    bool persistent_found = false;
                    bool delayed_found = false;
                    if (!set_prop_found)
                    {
                        foreach (object atr in propertyElement.Attributes)
                        {
                            IAttributeElement attr = atr as IAttributeElement;
                            if (attr != null && attr.Name == "Persistent")
                                persistent_found = true;

                            if (attr != null && attr.Name == "NonPersistent")
                                persistent_found = false;
                        
                            if (attr != null && attr.Name == "Delayed")
                                delayed_found = true;

                        }

                        if (!persistent_found)
                            continue;

                        ea.AddError(propertyElement.FirstNameRange, 
                            "Die persistente Property braucht " +
                            (delayed_found ? "SetDelayedPropertyValue" : "SetPropertyValue"));
                    }
                }
            }
            catch //(Exception ex)
            {

            }
        }

        private static bool ContainsSetProperty(string elementName, IEnumerable<IElement> elements, CheckCodeIssuesEventArgs ea)
        {
            if (elements == null) return false;
            try
            {
                foreach(IElement setterElement in elements)
                {
                    if (setterElement.ElementType == LanguageElementType.MethodCall
                                 && (
                                         setterElement.FullName.Contains("SetPropertyValue")
                                      || setterElement.FullName.Contains("SetDelayedPropertyValue")
                                      || setterElement.FullName.Contains("SetPropertyStringWithMaxSize"))
                                 )
                    {
                        IMethodCallStatement call = setterElement as IMethodCallStatement;
                        if (call != null)
                        {
                            foreach (var item in call.Arguments)
                            {
                                var i = item as IPrimitiveExpression;
                                if (i != null && i.Value.ToString() != elementName)
                                    ea.AddError(call.FirstNameRange,
                                        string.Format("Der Aufruf von SetPropertyValue benutzt nicht den Property-Namen !\r\nBitte \"{0}\" in \"{1}\" ändern !!!",
                                        i.Value, elementName));
                            }
                        }
                        return true;
                    }
                    if (ContainsSetProperty(elementName, setterElement.AllChildren, ea))
                        return true;
                }
            }
            catch 
            {
            }
            return false;
        }
    }
}