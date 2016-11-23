using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;

namespace spa
{
    public class Scenario
    {
        public string strScenario;
        public List<string> strCtrs;
    }

    public class Analysis
    {
        public List<Scenario> Scenarios;

        public Analysis(string ScenarioFilePath)
        {
            if (!File.Exists(ScenarioFilePath))
            {
                throw new System.IO.FileNotFoundException();
            }
            else
            {
                Scenarios = new List<Scenario>();
                XmlTextReader xr = null;
                string strCurElmt = "";
                try
                {
                    xr = new XmlTextReader(ScenarioFilePath);
                    xr.WhitespaceHandling = WhitespaceHandling.None;
                    //xr.Settings.IgnoreComments = ;
                    //xr.Settings.CheckCharacters = false;
                    while (xr.Read())
                    {
                        XmlNodeType nType = xr.NodeType;
                        if (nType == XmlNodeType.Element)
                        {
                            if (strCurElmt == "" && xr.Name.ToLower() == "spa_settings")
                            {
                                strCurElmt = "spa_settings";
                            }
                            if (strCurElmt == "spa_settings" && xr.Name.ToLower() == "spa_scenarios")
                            {
                                strCurElmt = "spa_settings\\spa_scenarios";
                            }
                            if (strCurElmt == "spa_settings\\spa_scenarios" && xr.Name.ToLower() == "scenario")
                            {
                                strCurElmt = "spa_settings\\spa_scenarios\\scenario";
                                Scenarios.Add(new Scenario());
                                Scenarios[Scenarios.Count - 1].strScenario = xr.GetAttribute("name");
                                Scenarios[Scenarios.Count - 1].strCtrs = new List<string>();
                            }
                            if (strCurElmt == "spa_settings\\spa_scenarios\\scenario" && xr.Name.ToLower() == "counter")
                            {
                                if (xr.GetAttribute("path") != null)
                                    Scenarios[Scenarios.Count - 1].strCtrs.Add(xr.GetAttribute("path"));
                            }
                        }
                        else if (nType == XmlNodeType.EndElement)
                        {
                            if (strCurElmt == "spa_settings" && xr.Name.ToLower() == "spa_settings")
                            {
                                strCurElmt = "";
                            }
                            if (strCurElmt == "spa_settings\\spa_scenarios" && xr.Name.ToLower() == "spa_scenarios")
                            {
                                strCurElmt = "spa_settings";
                            }
                            if (strCurElmt == "spa_settings\\spa_scenarios\\scenario" && xr.Name.ToLower() == "scenario")
                            {
                                strCurElmt = "spa_settings\\spa_scenarios";
                            }
                        }
                    }
                }
                finally
                {
                    if (xr != null)
                        xr.Close();
                }
            }
        }
    }
}
