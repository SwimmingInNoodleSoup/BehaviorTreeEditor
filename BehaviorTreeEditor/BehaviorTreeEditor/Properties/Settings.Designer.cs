﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace BehaviorTreeEditor.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "15.9.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("行为树编辑器")]
        public string EditorTitle {
            get {
                return ((string)(this["EditorTitle"]));
            }
            set {
                this["EditorTitle"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string WorkDirectory {
            get {
                return ((string)(this["WorkDirectory"]));
            }
            set {
                this["WorkDirectory"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("jingwen")]
        public string CopyRight {
            get {
                return ((string)(this["CopyRight"]));
            }
            set {
                this["CopyRight"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("NodeClass.xml")]
        public string NodeClassFile {
            get {
                return ((string)(this["NodeClassFile"]));
            }
            set {
                this["NodeClassFile"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("NodeDesignerData.xml")]
        public string NodeDesignerDataFile {
            get {
                return ((string)(this["NodeDesignerDataFile"]));
            }
            set {
                this["NodeDesignerDataFile"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute(".bytes")]
        public string NodeDataFileSuffix {
            get {
                return ((string)(this["NodeDataFileSuffix"]));
            }
            set {
                this["NodeDataFileSuffix"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string NodeDataSavePath {
            get {
                return ((string)(this["NodeDataSavePath"]));
            }
            set {
                this["NodeDataSavePath"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute(".workspace.xml")]
        public string WorkSpaceSetupSuffix {
            get {
                return ((string)(this["WorkSpaceSetupSuffix"]));
            }
            set {
                this["WorkSpaceSetupSuffix"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string WorkSpaceName {
            get {
                return ((string)(this["WorkSpaceName"]));
            }
            set {
                this["WorkSpaceName"] = value;
            }
        }
    }
}
