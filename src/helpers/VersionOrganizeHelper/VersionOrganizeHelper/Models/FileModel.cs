using System.IO;
using System.Xml;

namespace VersionOrganizeHelper.Models
{
    public class FileModel
    {
        public string FullName { get; set; }
        public string Name { get; set; }
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
            return string.Format("{0} - Version: {1}", this.Name, this.Version);
        }
    }
}
