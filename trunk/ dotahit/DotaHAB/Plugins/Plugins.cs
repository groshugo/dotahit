using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;
using System.Reflection;

namespace DotaHIT
{
    using Core;

    namespace Plugins    
    {
        public class PluginHandler
        {            
            public static void LoadPlugins()
            {
                string[] files = Directory.GetFiles(Application.StartupPath, "*.dll");                

                foreach (string file in files)
                {
                    try
                    {
                        Assembly assembly = Assembly.LoadFile(file);

                        foreach (Type type in assembly.GetTypes())
                            if ((type.GetInterface("IDotaHITPlugin") != null) && !type.IsAbstract)
                            {
                                IDotaHITPlugin plugin = (IDotaHITPlugin)Activator.CreateInstance(type);
                                Current.plugins[plugin.Type, plugin.Name] = plugin;
                            }
                    }
                    catch (Exception ex)
                    {
                        if (!(ex is BadImageFormatException))
                            MessageBox.Show("Couldn't load plugin: " + file + "\n" + ex.Message);
                    }
                }                
            }           
        } 

        public interface IDotaHITPlugin
        {
            string Type { get; }
            string Name { get; }
            UserControl Panel { get;}
            object Tag { get; set; }
        }
    }
}
