﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MISA.Web062023.AMIS.Domain.Resources.RecordingAsset {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class RecordingAsset {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal RecordingAsset() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("MISA.Web062023.AMIS.Domain.Resources.RecordingAsset.RecordingAsset", typeof(RecordingAsset).Assembly);
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
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Tạo mới chứng từ thành công..
        /// </summary>
        public static string CreateNewRecording {
            get {
                return ResourceManager.GetString("CreateNewRecording", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Tạo mới chứng từ thất bại.
        /// </summary>
        public static string CreateNewRecordingFail {
            get {
                return ResourceManager.GetString("CreateNewRecordingFail", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Không có tài sản nào được chọn để chứng từ.
        /// </summary>
        public static string NoAssetSelect {
            get {
                return ResourceManager.GetString("NoAssetSelect", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Không tồn tại tài sản nào ứng với chứng từ id {0} !!.
        /// </summary>
        public static string NoAssetWithRecording {
            get {
                return ResourceManager.GetString("NoAssetWithRecording", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Chứng từ với id {0} không tồn tại !!.
        /// </summary>
        public static string RecordingNotExist {
            get {
                return ResourceManager.GetString("RecordingNotExist", resourceCulture);
            }
        }
    }
}