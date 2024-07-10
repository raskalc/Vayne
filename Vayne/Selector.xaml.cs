using System.IO;
using System.Windows;
using Microsoft.Win32;
using Newtonsoft.Json;
using Vayne.Utils;

namespace Vayne;

public partial class Selector : Window
{
    public int Sel = 1;

    public Selector()
    {
        InitializeComponent();
    }

    private bool ValidateText()
    {
        if (Sel == 2 && (P1X.Text == ""
                         || P1Y.Text == ""
                         || P2X.Text == ""
                         || P2Y.Text == ""
                         || P3X.Text == ""
                         || P3Y.Text == "")
           )
            return false;

        if (Sel == 3 && (Sr.Text == ""
                         || Sx.Text == ""
                         || Sy.Text == ""
                         || Sz.Text == ""
            ))
            return false;
        return true;
    }

    private void OnSelected2d(object sender, RoutedEventArgs e)
    {
        Sel = 2;
        SphereGrid.Visibility = Visibility.Hidden;
        TriangleGrid.Visibility = Visibility.Visible;
    }

    private void OnSelected3d(object sender, RoutedEventArgs e)
    {
        Sel = 3;
        SphereGrid.Visibility = Visibility.Visible;
        TriangleGrid.Visibility = Visibility.Hidden;
    }

    private void SaveState(object sender, RoutedEventArgs e)
    {
        var state = new State();
        if (ValidateText() == false)
        {
            MessageBox.Show("You need enter correct values", "Error", MessageBoxButton.OK);
            return;
        }
        switch (Sel)
        {
            case 1:
            {
                MessageBox.Show("You need to select primitive", "Error", MessageBoxButton.OK);
                break;
            }
            case 2:
            {
                state.Selected = Sel;
                state.Point1X = int.Parse(P1X.Text);
                state.Point2X = int.Parse(P2X.Text);
                state.Point3X = int.Parse(P3X.Text);
                state.Point1Y = int.Parse(P1Y.Text);
                state.Point2Y = int.Parse(P2Y.Text);
                state.Point3Y = int.Parse(P3Y.Text);

                break;
            }
            case 3:
            {
                state.Selected = Sel;
                state.SphereRadius = int.Parse(Sr.Text);
                state.SphereX = int.Parse(Sx.Text);
                state.SphereY = int.Parse(Sy.Text);
                state.SphereZ = int.Parse(Sz.Text);
                break;
            }
        }

        var json = JsonConvert.SerializeObject(state);
        Console.WriteLine(json);
        var docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        using var outputFile = new StreamWriter(Path.Combine(docPath, "WriteLines.json"));
        outputFile.WriteLine(json);
        MessageBox.Show("Success save file", "Success", MessageBoxButton.OK);
    }

    private void LoadState(object sender, RoutedEventArgs e)
    {
        OpenFileDialog choofdlog = new OpenFileDialog();
        choofdlog.Filter = "Json Files (*.json)|*.json";
        choofdlog.FilterIndex = 1;
        choofdlog.Multiselect = true;

        if (choofdlog.ShowDialog() != true)
        {
            MessageBox.Show("Failed to open file", "Error", MessageBoxButton.OK);
        }
        string sFileName = choofdlog.FileName;
        using var file = new StreamReader(sFileName);
        var StateDeserializeObject = JsonConvert.DeserializeObject<State>(file.ReadLine());
        Sel = int.Parse(StateDeserializeObject.Selected.ToString());
        P1X.Text = StateDeserializeObject.Point1X.ToString();
        P1Y.Text = StateDeserializeObject.Point1Y.ToString();
        P2X.Text = StateDeserializeObject.Point2X.ToString();
        P2Y.Text = StateDeserializeObject.Point2Y.ToString();
        P3X.Text = StateDeserializeObject.Point3X.ToString();
        P3Y.Text = StateDeserializeObject.Point3Y.ToString();

        Sr.Text = StateDeserializeObject.SphereRadius.ToString();
        Sx.Text = StateDeserializeObject.SphereX.ToString();
        Sy.Text = StateDeserializeObject.SphereY.ToString();
        Sz.Text = StateDeserializeObject.SphereZ.ToString();
        if (Sel == 2)
        {
            OnSelected2d(null, null);
        }

        if (Sel == 3)
        {
            OnSelected3d(null,null);
        }
    }

    private void DrawCad(object sender, RoutedEventArgs e)
    {
        var aDraw = new Draw();
        switch (Sel)
        {
            case 2:
                aDraw.DrawTriangle(int.Parse(P1X.Text), int.Parse(P1Y.Text),int.Parse(P2X.Text), int.Parse(P2Y.Text),int.Parse(P3X.Text),int.Parse(P3Y.Text));
                break;
            case 3:
                aDraw.DrawSphere(int.Parse(Sr.Text), int.Parse(Sx.Text), int.Parse(Sy.Text), int.Parse(Sz.Text));
                break;
        }

    }
}