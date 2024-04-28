/*
 * XenoSave
 * Copyright (C) 2020-2023  damysteryman
 *
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU Affero General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU Affero General Public License for more details.
 *
 * You should have received a copy of the GNU Affero General Public License
 * along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.IO;

namespace dmm.XenoSave
{
    public class XCDEDataDB
    {
        public enum LANG
        {
            EN
        }

        public static readonly Dictionary<LANG, string> LANG_STR = new Dictionary<LANG, string>()
        {
            { LANG.EN, "EN" }
        };

        // public static string dbPathDir = "./";
        private static string dbPathFile = "/mnt/SPEEDY_BOI/Code/SaveHaxTools/XCDESaveHackThingy/XCDEData.sqlite";
        // public static string FullPath
        // {
        //     get
        //     {
        //         return $"{dbPathDir}{Path.DirectorySeparatorChar}{dbPathFile}";
        //     }
        // }
        // private SQLiteConnection dbConn = new SQLiteConnection($@"Data Source={FullPath}; Read Only=True;");
        private SQLiteConnection dbConn;
        //private static SQLiteConnection dbConn = new SQLiteConnection($@"Data Source={dbPathFile}; Read Only=True;");
        private SQLiteDataAdapter dbAdapter;
        private DataSet ds;

        public Dictionary<uint, string> ItemTypes { get; }
        public Dictionary<uint, string> Items { get; }


        public XCDEDataDB(LANG l = LANG.EN)
        {
            dbConn = new SQLiteConnection($@"Data Source={dbPathFile}; Read Only=True;");

            ItemTypes = ItemTypesDict(l);
            Items = ItemsDict(l);
        }

        /// <summary>
        /// Method for getting data from database and binding to DataSet
        /// </summary>
        /// <param name="query">SQL query to send to the database</param>
        public DataSet GetDataSet(string query)
        {
            ds = new DataSet();                             // Make new DataSet

            dbAdapter = new SQLiteDataAdapter(query, dbConn);  // Make new DbAdapter using supplied query
            dbConn.Open();                                  // Open connection to database
            dbAdapter.Fill(ds);                             // Fill the DataSet with obtained data
            dbConn.Close();                                 // Close connection to database

            return ds;
        }

        public DataTable GetItemTypes(LANG l = LANG.EN)
        {
            string query =
                "SELECT ID, " +
                "Name_" + LANG_STR[l] + " AS Name " +
                "FROM ItemTypes";

            return GetDataSet(query).Tables[0];
        }

        public Dictionary<uint, string> ItemTypesDict(LANG l = LANG.EN)
        {
            Dictionary<uint, string> output = new Dictionary<uint, string>();
            DataTable dt = GetItemTypes(l);
            foreach (DataRow r in dt.Rows)
                output.Add(Convert.ToUInt32(r["ID"]), (string)r["Name"]);
            return output;
        }

        public Dictionary<uint, string> ItemsDict(LANG l = LANG.EN)
        {
            Dictionary<uint, string> output = new Dictionary<uint, string>();
            DataTable dt = GetItems(l);
            foreach (DataRow r in dt.Rows)
                output.Add(Convert.ToUInt32(r["ID"]), (string)r["Name"]);
            return output;
        }

        public DataTable GetItems(LANG l = LANG.EN)
        {
            string query =
                "SELECT ID, " +
                "Name_" + LANG_STR[l] + " AS Name " +
                "FROM Items";

            return GetDataSet(query).Tables[0];
        }
    }
}
