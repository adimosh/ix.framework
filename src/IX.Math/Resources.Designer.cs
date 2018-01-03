//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace IX.Math {
    using global::System;
    using global::System.Reflection;

    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("IX.Math.Resources", typeof(Resources).GetTypeInfo().Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The requested function cannot be called with the specified operands, either because their number doesn&apos;t match the required number of operands, or because they have mismatched types..
        /// </summary>
        internal static string FunctionCallNotValid {
            get {
                return ResourceManager.GetString("FunctionCallNotValid", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The function &quot;{0}&quot; cannot be found..
        /// </summary>
        internal static string FunctionCouldNotBeFound {
            get {
                return ResourceManager.GetString("FunctionCouldNotBeFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The expression is either not logically sound, internally valid or its invocation was done with incorrect data..
        /// </summary>
        internal static string NotValidInternally {
            get {
                return ResourceManager.GetString("NotValidInternally", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The numerical type is either not a numerical type, or is not yet supported..
        /// </summary>
        internal static string NumericTypeInvalid {
            get {
                return ResourceManager.GetString("NumericTypeInvalid", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The parameter {0} should be an float value lest the expression makes no sense, but it is already defined as obligatory integer..
        /// </summary>
        internal static string ParameterMustBeFloatButAlreadyRequiredToBeInteger {
            get {
                return ResourceManager.GetString("ParameterMustBeFloatButAlreadyRequiredToBeInteger", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The parameter {0} should be an integer value lest the expression makes no sense, but it is already defined as obligatory float..
        /// </summary>
        internal static string ParameterMustBeIntegerButAlreadyRequiredToBeFloat {
            get {
                return ResourceManager.GetString("ParameterMustBeIntegerButAlreadyRequiredToBeFloat", resourceCulture);
            }
        }
    }
}
