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
using VorliasEngine2D.Entities.Components.Internal;

namespace VorliasEngine2D.System
{
    /// <summary>
    /// Experimental Prefab Serialization
    /// Note: This is a huge clusterfuck of code... that works.
    /// </summary>
    public class PrefabSerialization
    {
        const string COMPONENT_NAMESPACE = "VorliasEngine2D.Entities.Components";

        List<string> lines = new List<string>();
        Entity prefabEntity;
        int line = 0;
        int lineCount = 0;
        Mode mode;


        enum Mode
        {
            Main,
            Prefab,
            Entity,
            EntityChild,
            Component,
            Scripted,
        }

        /// <summary>
        /// The assemblies
        /// </summary>
        internal Assembly[] Assemblies
        {
            get
            {
                List<Assembly> assemblies = new List<Assembly>();

                foreach (Assembly a in AppDomain.CurrentDomain.GetAssemblies())
                {
                    if (!a.FullName.StartsWith("mscorlib")) 
                        assemblies.Add(a);
                }

                return assemblies.ToArray();
            }
        }

        internal Type[] ValidTypes
        {
            get
            {
                List<Type> validTypes = new List<Type>();
                Assemblies.ForEach(Assembly => {
                    foreach (Type t in Assembly.GetTypes())
                    {
                        if (t.IsSubclassOf(typeof(Component)) || t.IsSubclassOf(typeof(UIComponent)) || t.IsSubclassOf(typeof(EntityBehaviour)) )
                        {
                            validTypes.Add(t);
                        }
                    }
                });

                return validTypes.ToArray();
            }
        }

        internal Type[] GetTypesByName(string typeName)
        {
            return ValidTypes.Where(type => type.Name == typeName || type.FullName == typeName || type.Assembly.GetName() + type.Name == typeName).ToArray();
        }

        internal Type GetTypeByName(string typeName)
        {
            var types = GetTypesByName(typeName);
            return types?[0];
        }

        private string[] validPropertyTypes =
        {
            "string",
            "float",
            "int",
            "double",
            "UICoordinates",
            "Vector2",
            "Enum",
            "vertices",
            "bool",
            "boolean",
        };

        private void SetEnumAttribute(Type type, object instance, string propertyName, string value)
        {
            try
            {
                var property = type.GetProperty(propertyName);
                var attr = property.GetCustomAttribute<SerializablePropertyAttribute>();

                if (attr != null)
                    property.SetValue(instance, Enum.Parse(property.PropertyType, value));
                else
                    Console.WriteLine("Unable to write property: " + propertyName);
            }
            catch (NullReferenceException e)
            {
                Console.WriteLine("Invalid property: " + propertyName + " (Enum) for " + type.AssemblyQualifiedName);
            }
            catch (ArgumentNullException e2)
            {
                Console.WriteLine("Invalid property: " + propertyName + " (Enum) for " + type.AssemblyQualifiedName);
            }
        }

        private void SetBoolAttribute(Type type, object instance, string propertyName, bool value)
        {
            try
            {
                var property = type.GetProperty(propertyName);
                var attr = property.GetCustomAttribute<SerializablePropertyAttribute>();

                if (attr != null)
                    property.SetValue(instance, value);
                else
                    Console.WriteLine("Unable to write property: " + propertyName);
            }
            catch (NullReferenceException e)
            {
                Console.WriteLine("Invalid property: " + propertyName + " (bool) for " + type.AssemblyQualifiedName);
            }
            catch (ArgumentNullException e2)
            {
                Console.WriteLine("Invalid property: " + propertyName + " (bool) for " + type.AssemblyQualifiedName);
            }
        }

        private void SetUICoordinatesAttribute(Type type, object instance, string propertyName, float scaleX, float offsetX, float scaleY, float offsetY)
        {
            try
            {
                var property = type.GetProperty(propertyName);
                var attr = property.GetCustomAttribute<SerializablePropertyAttribute>();

                if (attr != null)
                    property.SetValue(instance, new UICoordinates(scaleX, offsetX, scaleY, offsetY));
                else
                    Console.WriteLine("Unable to write property: " + propertyName);
            }
            catch (NullReferenceException e)
            {
                Console.WriteLine("Invalid property: " + propertyName + " (UICoordinates) for " + type.AssemblyQualifiedName);
            }
            catch (ArgumentNullException e2)
            {
                Console.WriteLine("Invalid property: " + propertyName + " (UICoordinates) for " + type.AssemblyQualifiedName);
            }
        }

        private void SetVector2Attribute(Type type, object instance, string propertyName, int x, int y)
        {
            

            try
            {
                var property = type.GetProperty(propertyName);
                var attr = property.GetCustomAttribute<SerializablePropertyAttribute>();

                if (attr != null)
                    property.SetValue(instance, new Vector2f(x, y));
                else
                    Console.WriteLine("Unable to write property: " + propertyName);
            }
            catch (NullReferenceException e)
            {
                Console.WriteLine("Invalid property: " + propertyName + " (Vector2) for " + type.AssemblyQualifiedName);
            }
        }


        private void SetRawAttribute<T>(Type type, object instance, string propertyName, T x)
        {
            try
            {
                var property = type.GetProperty(propertyName);

                var attr = property?.GetCustomAttribute<SerializablePropertyAttribute>();

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

        private void SetPolygonAttribute(Type type, object instance, string propertyName, Polygon newPoly)
        {
            try
            {
                var property = type.GetProperty(propertyName);
                var attr = property.GetCustomAttribute<SerializablePropertyAttribute>();

                if (attr != null)
                    property.SetValue(instance, newPoly);
                else
                    Console.WriteLine("Unable to write property: " + propertyName);
            }
            catch (NullReferenceException e)
            {
                Console.WriteLine("Invalid property: " + propertyName + " (polygon) for " + type.AssemblyQualifiedName);
            }
        }

        private void SetStringAttribute(Type type, object instance, string propertyName, string value)
        {
            
            try
            {
                var property = type.GetProperty(propertyName);
                var attr = property.GetCustomAttribute<SerializablePropertyAttribute>();

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

        private void ParseComponent(string componentName, Entity targetEntity, string ns = "", Assembly assembly = null)
        {
            try
            {
                Type componentType;
                if (assembly != null)
                {
                    componentType = assembly.GetType(ns + componentName, true);
                }
                else
                {
                    componentType = GetTypeByName(ns + componentName); //Type.GetType(ns + componentName, true);
                }

                IComponent com;
                targetEntity.FindOrCreateComponent(componentType, out com);

                if (com == null)
                    throw new Exception("Tried adding component of  type: " + componentName + " to " + targetEntity.Name);

                while (mode == Mode.Component && line < lineCount)
                {
                    string next = lines[line];
                    StringTokenizer tokenizer = new StringTokenizer(next);
                    string key = tokenizer.Read();
                    bool isType = key.InArray(validPropertyTypes);

                    line++;

                    if (isType || key == "property" || key == "attr" || key == "attribute")
                    {
                        string type = isType ? key : tokenizer.Read();
                        if (type == "vec2" || type == "Vector2")
                        {
                            string name = tokenizer.Read();
                            int x = tokenizer.ReadInt();
                            int y = tokenizer.ReadInt();
                            SetVector2Attribute(componentType, com, name, x, y);
                        }
                        else if (type == "boolean" || type == "bool")
                        {
                            string name = tokenizer.Read();
                            string value = tokenizer.Read();
                            SetBoolAttribute(componentType, com, name, !value.ToLower().Equals("false"));
                        }
                        else if (type == "vertices")
                        {
                            string name = tokenizer.Read();
                            line++;
                            Polygon newPoly;
                            ParsePolygon(out newPoly);
                            SetPolygonAttribute(componentType, com, name, newPoly);
                        }
                        else if (type == "uicoords" || type == "UICoordinates")
                        {
                            string name = tokenizer.Read();

                            float x1 = tokenizer.ReadFloat();
                            float x2 = tokenizer.ReadFloat();
                            float y1 = tokenizer.ReadFloat();
                            float y2 = tokenizer.ReadFloat();
                            SetUICoordinatesAttribute(componentType, com, name, x1, x2, y1, y2);
                        }
                        else if (type == "float")
                        {
                            string name = tokenizer.Read();
                            float x = tokenizer.ReadFloat();

                            SetRawAttribute(componentType, com, name, x);
                        }
                        else if (type == "int")
                        {
                            string name = tokenizer.Read();
                            int x = tokenizer.ReadInt();

                            SetRawAttribute(componentType, com, name, x);
                        }
                        else if (type == "uint")
                        {
                            string name = tokenizer.Read();
                            int x = tokenizer.ReadInt();

                            SetRawAttribute(componentType, com, name, (uint) x);
                        }
                        else if (type == "string")
                        {
                            string name = tokenizer.Read();
                            string value = tokenizer.ReadLine();

                            SetStringAttribute(componentType, com, name, value);
                        }
                        else if (type == "enum" || type == "Enum")
                        {
                            string name = tokenizer.Read();
                            string value = tokenizer.ReadLine();

                            SetEnumAttribute(componentType, com, name, value);
                        }
                        else
                        {
                            Console.WriteLine("Unknown: " + type);
                        }
                    }
                    else if (key == "@debug") // debug component
                    {
                        try
                        {
                            Console.WriteLine("\t### == DEBUG == ");
                            var properties = com.GetType().GetProperties();
                            foreach (var property in properties)
                            {
                                if (property.CanRead)
                                    Console.WriteLine("\t###\t{0} = {1}", property.Name, property.GetValue(com));

                            }
                        }
                        catch (NullReferenceException e)
                        {

                        }
                    }
                    else if (key == "end")
                    {
                        mode = Mode.Entity;
                        break;
                    }

                
                }

            }
            catch (TypeLoadException e)
            {
                Console.Error.WriteLine("Failed to load component: `" + componentName + "` under `" + ns + "`");
                Environment.Exit(0xDEADC0);
            }
        }



        private void ParsePolygon(out Polygon polygon)
        {
            polygon = new Polygon();
            while (line < lineCount)
            {
                string next = lines[line];
                StringTokenizer tokenizer = new StringTokenizer(next);
                string key = tokenizer.Read();
                if (key == "end")
                {
                    line++;
                    break;
                }
                else if (key == "vertex" || key == "Vector2")
                {
                    int x = tokenizer.ReadInt();
                    int y = tokenizer.ReadInt();
                    polygon.Add(new Vector2f(x, y));
                    line++;
                }
                else
                {
                    throw new InvalidDataException("Unknown vertex syntax on line: " + next);
                }
            }
        }

        private void ParseEntityChild(Entity instance, string childName)
        {
            if (childName != "")
                instance.Name = childName;

            while (line < lineCount)
            {
                string next = lines[line];
                StringTokenizer tokenizer = new StringTokenizer(next);
                string key = tokenizer.Read();
                bool isType = key.InArray(validPropertyTypes);

                line++;

                if (isType || key == "property" || key == "attr" || key == "attribute")
                {
                    string type =  isType ? key : tokenizer.Read();
                    if (type == "string")
                    {
                        string attribName = tokenizer.Read();
                        SetStringAttribute(prefabEntity.GetType(), instance, attribName, tokenizer.ReadLine());
                    }
                    else
                    {
                        throw new InvalidDataException("Invalid type: " + type);
                    }
                }
                else if (key == "component")
                {
                    mode = Mode.Component;
                    ParseComponent(tokenizer.Read(), instance, COMPONENT_NAMESPACE + ".");
                }
                else if (key == "end" || key == ";")
                {
                    break;
                }
                else if (key == "scripted")
                {
                    mode = Mode.Component;
                    ParseComponent(tokenizer.Read(), instance, "", Assembly.GetEntryAssembly());
                }
                else if (key == "entity")
                {
                    // child entity
                    ParseEntityChild(Entity.Create(instance), tokenizer.Read());
                }
                else if (key == "@debug")
                {
                    Console.WriteLine("\t### == DEBUG " + instance.FullName +" == ");

                    foreach (var component in instance.Components)
                    {
                        Console.WriteLine("\t###\tCOMPONENT " + component.Name);
                    }
                }
            }
        }

        private void ParseEntity(string entityName)
        {
            if (entityName != "")
                prefabEntity.Name = entityName;

            while (mode == Mode.Entity && line < lineCount)
            {
                string next = lines[line];
                StringTokenizer tokenizer = new StringTokenizer(next);
                string key = tokenizer.Read();
                bool defaultMode = false;

                

                line++;

                if (key == "default")
                {
                    defaultMode = true;
                    key = tokenizer.Read();
                }

                if (key == "property" || key == "attr" || key == "attribute")
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
                    ParseComponent(tokenizer.Read(), prefabEntity, COMPONENT_NAMESPACE + ".");
                }
                else if (key == "end" || key == ";")
                {
                    mode = Mode.Main;
                    break;
                }
                else if (key == "scripted")
                {
                    mode = Mode.Component;
                    ParseComponent(tokenizer.Read(), prefabEntity, "", null);
                }
                else if (key == "entity")
                {
                    ParseEntityChild(Entity.Create(prefabEntity), tokenizer.Read());
                }

                if (defaultMode)
                {
                    mode = Mode.Main;
                    break;
                }
            }
        }

        public void RunLexer()
        {
            bool isParsing = false;
            while (line < lineCount)
            {
                string next = lines[line];
                StringTokenizer tokenizer = new StringTokenizer(next);
                string key = tokenizer.Read();
                

                line++;


                if (key.StartsWith("#prefab"))
                {
                    isParsing = true;
                }
                else if (key == "entity")
                {
                    isParsing = true;
                    mode = Mode.Entity;
                    ParseEntity(tokenizer.Read());
                }
                else if (key.StartsWith("--") || !isParsing)
                {
                    // ignore comment
                }
                else if (key.StartsWith("#end"))
                {
                    isParsing = false;
                    break;
                }
                else
                {
                    Console.WriteLine("Unknown: '" + key + "' @line " + line + " have you parsed?");

                }

                
            }
        }

        public Prefab ToPrefab()
        {
            return Prefab.Create(prefabEntity);
        }

        public PrefabSerialization(StreamReader file)
        {
            prefabEntity = Entity.Create();
            prefabEntity.IsPrefab = true;
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
            prefabEntity = Entity.Create();
            prefabEntity.IsPrefab = true;
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
