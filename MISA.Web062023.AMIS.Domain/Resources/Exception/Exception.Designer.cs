﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MISA.Web062023.AMIS.Domain.Resources.Exception {
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
    public class Exception {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Exception() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("MISA.Web062023.AMIS.Domain.Resources.Exception.Exception", typeof(Exception).Assembly);
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
        ///   Looks up a localized string similar to Dữ liệu gửi đến không hợp lệ.
        /// </summary>
        public static string BadRequestException {
            get {
                return ResourceManager.GetString("BadRequestException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Các mã {0} đã tồn tại. Vui lòng nhập mã khác.
        /// </summary>
        public static string CodesExist {
            get {
                return ResourceManager.GetString("CodesExist", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Dữ liệu gửi đến bị trùng.
        /// </summary>
        public static string ConflictException {
            get {
                return ResourceManager.GetString("ConflictException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Mã {0} đã tồn tại. Vui lòng nhập mã khác.
        /// </summary>
        public static string DuplicateCode {
            get {
                return ResourceManager.GetString("DuplicateCode", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Bản ghi với id {0} không tồn tại.
        /// </summary>
        public static string NotExistId {
            get {
                return ResourceManager.GetString("NotExistId", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Không tìm thấy dữ liệu.
        /// </summary>
        public static string NotFoundException {
            get {
                return ResourceManager.GetString("NotFoundException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Lỗi từ server. Thử lại sau .
        /// </summary>
        public static string Server {
            get {
                return ResourceManager.GetString("Server", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Các mã {0} đã tồn tại.
        /// </summary>
        public static string SomeIdsExist {
            get {
                return ResourceManager.GetString("SomeIdsExist", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Không tồn tại các Id: {0}.
        /// </summary>
        public static string SomeIdsNotExist {
            get {
                return ResourceManager.GetString("SomeIdsNotExist", resourceCulture);
            }
        }
    }
}