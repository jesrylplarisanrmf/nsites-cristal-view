﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace JCSoftwares_V.Global
{
    class GlobalClassHandler
    {
        public Type createObjectFromClass(string pObject)
        {
            string _URL = "";
            switch (pObject)
            { 
                case "Supplier": _URL = "JCSoftwares_V.ApplicationObjects.Classes.Inventorys";
                    break;
                case "Customer": _URL = "JCSoftwares_V.ApplicationObjects.Classes.Inventorys";
                    break;
                case "User": _URL = "JCSoftwares_V.ApplicationObjects.Classes.Systems";
                    break;
                case "SalesPerson": _URL = "JCSoftwares_V.ApplicationObjects.Classes.Sales";
                    break;
                case "InventoryGroup": _URL = "JCSoftwares_V.ApplicationObjects.Classes.Inventorys";
                    break;
                case "Category": _URL = "JCSoftwares_V.ApplicationObjects.Classes.Inventorys";
                    break;
                case "Unit": _URL = "JCSoftwares_V.ApplicationObjects.Classes.Inventorys";
                    break;
                case "Location": _URL = "JCSoftwares_V.ApplicationObjects.Classes.Inventorys";
                    break;
                case "Classification": _URL = "JCSoftwares_V.ApplicationObjects.Classes.Accountings";
                    break;
                case "SubClassification": _URL = "JCSoftwares_V.ApplicationObjects.Classes.Accountings";
                    break;
                case "MainAccount": _URL = "JCSoftwares_V.ApplicationObjects.Classes.Accountings";
                    break;
                case "Employee": _URL = "JCSoftwares_V.ApplicationObjects.Classes.HRISs";
                    break;
                case "UserGroup": _URL = "JCSoftwares_V.ApplicationObjects.Classes.Systems";
                    break;
            }
            
            Type _Type = null;
            Type[] typelist = GetTypesInNamespace(Assembly.GetExecutingAssembly(), _URL);
            for (int i = 0; i < typelist.Length; i++)
            {
                if (pObject == typelist[i].Name)
                {
                    _Type = typelist[i];
                }
            }

            return _Type;
        }

        public Type[] GetTypesInNamespace(Assembly assembly, string nameSpace)
        {
            return assembly.GetTypes().Where(t => String.Equals(t.Namespace, nameSpace, StringComparison.Ordinal)).ToArray();
        }
    }
}
