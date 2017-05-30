using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VorliasEngine2D.Entities;
using VorliasEngine2D.System.Utility;
using VorliasEngine2D.Entities.Components;
using SFML.System;
using VorliasEngine2D.Serialization;

namespace VorliasEngine2D.System
{
    class EntitySerialization
    {

    }

    public class PrefabSerialization
    {
        List<string> lines = new List<string>();
        Entity prefabEntity;
        int line = 0;
        int lineCount = 0;
        Mode mode;

        enum Mode
        {
            Main,
            Entity,
            Component,
            Scripted,
        }

        private void SetVector2Attribute(Type type, object instance, string propertyName, int x, int y)
        {
            

            try
            {
                var property = type.GetProperty(propertyName);
                var attr = property.GetCustomAttribute<PersistentPropertyAttribute>();

                if (attr != null)
                    property.SetValue(instance, new Vector2f(x, y));
                else
                    Console.WriteLine("Unable to write property: " + propertyName);
            }
            catch (NullReferenceException e)
            {
                Console.WriteLine("Invalid property: " + propertyName + " (vec2) for " + type.AssemblyQualifiedName);
            }
        }


        private void SetFloatAttribute(Type type, object instance, string propertyName, float x)
        {
            try
            {
                var property = type.GetProperty(propertyName);
                var attr = property.GetCustomAttribute<PersistentPropertyAttribute>();

                if (attr != null)
                    property.SetValue(instance, x);
                else
                    Console.WriteLine("Unable to write property: " + propertyName);
            }
            catch (NullReferenceException e)
            {
                Console.WriteLine("Invalid property: " + propertyName + " (float) for " + type.AssemblyQualifiedName);
            }
        }

        private void SetStringAttribute(Type type, object instance, string propertyName, string value)
        {
            
            try
            {
                var property = type.GetProperty(propertyName);
                var attr = property.GetCustomAttribute<PersistentPropertyAttribute>();

                if (attr != null)
                    property.SetValue(instance, value);
                else
                    Console.WriteLine("Unable to write property: " + propertyName);
            }
            catch (NullReferenceException e)
            {
                Console.WriteLine("Invalid property: " + propertyName);
            }
            
        }

        private void ParseComponent(string componentName, string ns = "", Assembly assembly = null)
        {
            //Assembly ass = Assembly.Load("VE2D");
            Type componentType;
            if (assembly != null)
            {
                componentType = assembly.GetType(ns + componentName, true);
            }
            else
            {
                componentType = Type.GetType(ns + componentName, true);
            }

            IComponent com;
            prefabEntity.AddComponent(componentType, out com);

            

            while (mode == Mode.Component && line < lineCount)
            {
                string next = lines[line];
                StringTokenizer tokenizer = new StringTokenizer(next);
                string key = tokenizer.Read();

                line++;

                if (key == "property")
                {
                    string type = tokenizer.Read();
                    if (type == "vec2")
                    {
                        string name = tokenizer.Read();
                        int x = tokenizer.ReadInt();
                        int y = tokenizer.ReadInt();
                        Console.WriteLine("{0} = ({1}, {2})", name, x, y);
                        SetVector2Attribute(componentType, com, name, x, y);
                    }
                    else if (type == "float")
                    {
                        string name = tokenizer.Read();
                        float x = tokenizer.ReadFloat();

                        Console.WriteLine("{0} = {1}f", name, x);
                        SetFloatAttribute(componentType, com, name, x);
                    }
                    else if (type == "string")
                    {
                        string name = tokenizer.Read();
                        string value = tokenizer.ReadLine();

                        SetStringAttribute(componentType, com, name, value);
                    }
                    else
                    {
                        Console.WriteLine("Unknown: " + type);
                    }
                }
                else if (key == "end")
                {
                    mode = Mode.Entity;
                    break;
                }

                
            }
        }


        private void ParseEntity()
        {
            while (mode == Mode.Entity && line < lineCount)
            {
                string next = lines[line];
                StringTokenizer tokenizer = new StringTokenizer(next);
                string key = tokenizer.Read();

                line++;

                if (key == "property")
                {
                    string type = tokenizer.Read();
                    if (type == "string")
                    {
                        string attribName = tokenizer.Read();
                        SetStringAttribute(prefabEntity.GetType(), prefabEntity, attribName, tokenizer.ReadLine());
                    }
                    else
                    {
                        throw new InvalidDataException("Invalid type: " + type);
                    }
                }
                else if (key == "component")
                {
                    mode = Mode.Component;
                    ParseComponent(tokenizer.Read(), "VorliasEngine2D.Entities.Components.");
                }
                else if (key == "end")
                {
                    mode = Mode.Main;
                    break;
                }
                else if (key == "scripted")
                {
                    mode = Mode.Component;
                    ParseComponent(tokenizer.Read(), "", Assembly.GetEntryAssembly());
                }

                
            }
        }

        public void RunLexer()
        {
            while (line < lineCount)
            {
                string next = lines[line];
                StringTokenizer tokenizer = new StringTokenizer(next);
                string key = tokenizer.Read();

                line++;

                if (key.StartsWith("#"))
                {
                    // ignore comment
                }
                else if (key == "entity")
                {
                    mode = Mode.Entity;
                    ParseEntity();
                }
                else
                {
                    Console.WriteLine("Unknown: '" + key + "' @line " + line );
                }

                
            }
        }

        public Prefab ToPrefab()
        {
            return Prefab.Create(prefabEntity);
        }

        public PrefabSerialization(StreamReader file)
        {
            prefabEntity = Entity.Spawn();
            string line;

            while ((line = file.ReadLine()) != null)
            {
                lines.Add(line);
                lineCount++;
            }

            file.Close();
        }

        public PrefabSerialization(StringReader reader)
        {
            prefabEntity = Entity.Spawn();
            string line;

            while ((line = reader.ReadLine()) != null)
            {
                lines.Add(line);
                lineCount++;
            }

            reader.Close();
        }
    }
}
