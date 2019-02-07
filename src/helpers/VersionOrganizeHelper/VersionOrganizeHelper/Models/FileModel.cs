using System;
using System.IO;
using System.Xml;
using VersionOrganizeHelper.Definitions;

namespace VersionOrganizeHelper.Models
{
    public class FileModel
    {
        public string FullName { get; set; }
        public string Name { get; set; }

        public string LastModifyItem { get; set; }
        public DateTime LastModifyItemDate { get; set; }

        public string Version
        {
            get
            {
                if (this.VersionNode != null)
                    return this.VersionNode.InnerText;
                return string.Empty;
            }
        }
        public XmlDocument XmlDocument { get; set; }
        private XmlNode VersionNode
        {
            get
            {
                var selectNodes = XmlDocument.SelectNodes("/Project/PropertyGroup/Version");
                if (selectNodes.Count > 0)
                    return selectNodes[0];

                return null;
            }
        }

        public bool UpdateVersion(params int[] values)
        {
            var content = File.ReadAllText(this.FullName);
            var version = string.Concat("<Version>", this.Version, "</Version>");
            var versionNew = string.Concat("<Version>", string.Join(".", values), "</Version>");

            if (content.IndexOf(version) != -1)
            {
                content = content.Replace(version, versionNew);
                File.WriteAllText(this.FullName, content);

                return true;
            }

            return false;
        }

        public override string ToString()
        {
            var lasModify = string.Format("{0} - {1:" + HelperDefinition.DATE_FORMAT_ALL + "}", LastModifyItemDate, LastModifyItem);
            return string.Format("{0} - Version: {1}  - ({2})", this.Name, this.Version, lasModify);
        }
    }
}
