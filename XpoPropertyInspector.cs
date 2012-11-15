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

                        foreach (IElement setterElement in propertyElement.SetMethod.AllChildren)
                        {
                            if (setterElement.ElementType == LanguageElementType.MethodCall && setterElement.FullName.Contains("SetPropertyValue"))
                            {
                                set_prop_found = true;
                                IMethodCallStatement call = setterElement as IMethodCallStatement;
                                if (call != null)
                                {
                                    foreach (var item in call.Arguments)
                                    {
                                        var i = item as IPrimitiveExpression;
                                        if (i != null && i.Value.ToString() != element.Name)
                                            ea.AddError(call.FirstNameRange,
                                                string.Format("Der Aufruf von SetPropertyValue benutzt nicht den Property-Namen !\r\nBitte \"{0}\" in \"{1}\" ändern !!!",
                                                i.Value, element.Name));
                                    }
                                }
                            }
                        }
                    }

                    bool persistent_found = false;
                    if (!set_prop_found)
                    {
                        foreach (object atr in propertyElement.Attributes)
                        {
                            IAttributeElement attr = atr as IAttributeElement;
                            if (attr != null && attr.Name == "Persistent")
                            {
                                persistent_found = true;
                                break;
                            }
                        }
                        if (persistent_found)
                            ea.AddError(propertyElement.FirstNameRange,
                                "Die persistente Property braucht SetPropertyValue");
                    }

                }
            }
            catch //(Exception ex)
            {

            }
        }
	}
}