using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.IO;
using System.Windows;
using System.Xml;
using VersionOrganizeHelper.Models;

namespace VersionOrganizeHelper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const char FOLDERS_COMMA = ';';
        private const char VERSION_COMMA = '.';
        private const string SEARCH_PATTERN_CSPROJ = "*.csproj";
        private const string RESULT_TEXT = "{0} file(s) affected";
        private string DefaultSearchFolders
        {
            get
            {
                return ConfigurationManager.AppSettings["DefaultSearchFolders"] as string;
            }
        }

        private IDictionary<string, FileModel> csprojCollection;
        private IDictionary<string, FileModel> CSPROJCollection
        {
            get
            {
                if (this.csprojCollection == null)
                    this.csprojCollection = new Dictionary<string, FileModel>();

                return this.csprojCollection;
            }
        }

        private IEnumerable<FileModel> SelectedItems
        {
            get
            {
                foreach (var item in this.lbxprojects.SelectedItems)
                    yield return (FileModel)item;

            }
        }

        public MainWindow()
        {
            this.InitializeComponent();

            this.txtfolders.Text = this.DefaultSearchFolders;
            this.ReadProjects();
        }

        private void ReadProjects()
        {
            var values = this.txtfolders.Text.Split(FOLDERS_COMMA);
            foreach (var folder in values)
                this.ReadProject(folder);
        }

        private void ReadProject(string path, bool clearList = true)
        {
            if (clearList)
                this.lbxprojects.Items.Clear();

            if (string.IsNullOrEmpty(path))
                return;

            var directoryInfo = new DirectoryInfo(path);

            if (!directoryInfo.Exists)
                return;

            foreach (var file in directoryInfo.GetFiles(SEARCH_PATTERN_CSPROJ, SearchOption.AllDirectories))
            {
                var value = new FileModel
                {
                    FullName = file.FullName,
                    Name = file.Name,
                    XmlDocument = this.LoadXml(file.FullName)
                };

                this.lbxprojects.Items.Add(value);

                if (!this.CSPROJCollection.ContainsKey(value.FullName))
                    this.CSPROJCollection.Add(file.FullName, value);
            }
        }

        private XmlDocument LoadXml(string path)
        {
            var doc = new XmlDocument();
            doc.Load(path);
            return doc;
        }

        private void btnReadFolders_Click(object sender, RoutedEventArgs e)
        {
            this.ReadProjects();
        }

        private void btnChange_Click(object sender, RoutedEventArgs e)
        {
            var affecteds = 0;
            foreach (var item in this.SelectedItems)
            {
                if (string.IsNullOrEmpty(item.Version))
                    continue;

                if (string.IsNullOrEmpty(this.txtVersion.Text))
                    continue;

                var versionSplit = this.txtVersion.Text.Split(VERSION_COMMA);

                var v1 = int.Parse(versionSplit[0]);
                var v2 = int.Parse(versionSplit[1]);
                var v3 = int.Parse(versionSplit[2]);

                if (item.UpdateVersion(v1, v2, v3))
                    affecteds++;
            }

            this.ReadProjects();
            this.txtResult.Content = string.Format(RESULT_TEXT, affecteds);
        }

        private void btnVersionUpdate_Click(object sender, RoutedEventArgs e)
        {
            var affecteds = 0;

            foreach (var item in this.SelectedItems)
            {
                if (string.IsNullOrEmpty(item.Version))
                    continue;

                var versionSplit = item.Version.Split(VERSION_COMMA);

                var v1 = int.Parse(versionSplit[0]);
                var v2 = int.Parse(versionSplit[1]);
                var v3 = int.Parse(versionSplit[2]);

                if (cbkVersion1.IsChecked == true)
                    v1++;

                if (cbkVersion2.IsChecked == true)
                    v2++;

                if (cbkVersion3.IsChecked == true)
                    v3++;

                if (item.UpdateVersion(v1, v2, v3))
                    affecteds++;
            }

            this.ReadProjects();
            this.txtResult.Content = string.Format(RESULT_TEXT, affecteds);

        }

        private void cbxListAll_Checked(object sender, RoutedEventArgs e)
        {
            this.lbxprojects.SelectAll();
        }

        private void cbxListAll_Unchecked(object sender, RoutedEventArgs e)
        {
            this.lbxprojects.UnselectAll();
        }
    }
}
