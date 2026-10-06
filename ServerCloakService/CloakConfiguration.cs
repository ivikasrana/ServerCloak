using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ServerCloakService
{
    internal class BaseConfig
    {
        internal void CloneFrom(BaseConfig source)
        {
            foreach (PropertyInfo info in base.GetType().GetProperties())
            {
                if (info.CanWrite)
                {
                    info.SetValue(this, info.GetValue(source, null), null);
                }
            }
        }
    }

    internal class CloakConfiguration
    {
        private BaseConfig _CloakSettings;

        internal void CloneFrom(CloakConfiguration source)
        {
            this.AssemblyName = source.AssemblyName;
            this.CloakName = source.CloakName;
            this.ConfigurationSettingsTypeName = source.ConfigurationSettingsTypeName;
            if (!string.IsNullOrEmpty(this.ConfigurationSettingsTypeName) && (source.CloakSettings != null))
            {
                this.CloakSettings = (BaseConfig)Activator.CreateInstance(this.GetConfigurationType());
                this.CloakSettings.CloneFrom(source.CloakSettings);
            }
        }

        internal Type GetConfigurationType()
        {
            if (File.Exists(this.AssemblyName))
            {
                Assembly assembly = Assembly.LoadFile(this.AssemblyName);
                if (assembly != null)
                {
                    try
                    {
                        foreach (Type type in assembly.GetTypes())
                        {
                            if ((type.IsPublic && !type.IsAbstract) && (type.FullName == this.ConfigurationSettingsTypeName))
                            {
                                return type;
                            }
                        }
                    }
                    catch (Exception exception)
                    {
                        throw exception;
                    }
                }
            }
            return null;
        }

        internal string CloakName { get; set; }

        [XmlIgnore]
        internal BaseConfig CloakSettings
        {
            get
            {
                if ((this._CloakSettings == null) && !string.IsNullOrEmpty(this.ConfigurationSettingsTypeName))
                {
                    try
                    {
                        Type configurationType = this.GetConfigurationType();
                        if (configurationType != null)
                        {
                            object obj2 = Activator.CreateInstance(configurationType);
                            if (obj2 is BaseConfig)
                            {
                                this._CloakSettings = (BaseConfig)obj2;
                            }
                        }
                    }
                    catch (Exception exception)
                    {
                        throw exception;
                    }
                }
                return this._CloakSettings;
            }
            set
            {
                this._CloakSettings = value;
            }
        }

        internal string AssemblyName { get; set; }

        internal string ConfigurationSettingsTypeName { get; set; }
    }
}
