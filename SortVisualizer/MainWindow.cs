using System;
using Gtk;
using Cairo;
using System.Threading;

public partial class MainWindow : Gtk.Window
{

    int[] data = { 20, 30, 40, 50, 60, 70, 80, 90, 100, 110 };
    DrawingArea area;

    public MainWindow() : base(Gtk.WindowType.Toplevel)
    {
        Build();
    }

    protected void OnDeleteEvent(object sender, DeleteEventArgs a)
    {
        Application.Quit();
        a.RetVal = true;
    }

    void Draw()
    {
        using (Cairo.Context g = Gdk.CairoHelper.Create(area.GdkWindow))
        {
            g.SetSourceColor(new Color(0, 0, 0));

            g.Rectangle(0, 0, 500, 300);

            g.Fill();

            g.SetSourceColor(new Color(1, 1, 1));

            for (int x = 0; x < data.Length; x++)
            {
                g.Rectangle(x * (500 / data.Length) + 1, 0, (500 / data.Length) - 1, data[x]);
            }

            g.Fill();

            g.GetTarget().Dispose();
        }
    }

    int partition(int[] arr, int low, int high)
    {
        int pivot = arr[high];

        // index of smaller element 
        int i = (low - 1);
        for (int j = low; j < high; j++)
        {
            // If current element is smaller  
            // than the pivot 
            if (arr[j] < pivot)
            {
                i++;

                // swap arr[i] and arr[j] 
                int temp = arr[i];
                arr[i] = arr[j];
                arr[j] = temp;
                Draw();
                // A sleep so we can see what is realy happening
                Thread.Sleep(5);
            }
        }

        // swap arr[i+1] and arr[high] (or pivot) 
        int temp1 = arr[i + 1];
        arr[i + 1] = arr[high];
        arr[high] = temp1;

        return i + 1;
    }


    /* The main function that implements QuickSort() 
    arr[] --> Array to be sorted, 
    low --> Starting index, 
    high --> Ending index */
    void quickSort(int[] arr, int low, int high)
    {
        if (low < high)
        {

            /* pi is partitioning index, arr[pi] is  
            now at right place */
            int pi = partition(arr, low, high);

            // Recursively sort elements before 
            // partition and after partition 
            quickSort(arr, low, pi - 1);
            quickSort(arr, pi + 1, high);
        }
    }

    void Buble()
    {
        int temp;
        for (int j = 0; j <= data.Length - 2; j++)
        {
            for (int i = 0; i <= data.Length - 2; i++)
            {
                if (data[i] > data[i + 1])
                {
                    temp = data[i + 1];
                    data[i + 1] = data[i];
                    data[i] = temp;
                    Draw();
                }
            }
        }
    }

    void Insertion()
    {
        int i, j, val, flag;
        for (i = 1; i < data.Length; i++)
        {
            val = data[i];
            flag = 0;
            for (j = i - 1; j >= 0 && flag != 1;)
            {
                if (val < data[j])
                {
                    data[j + 1] = data[j];
                    j--;
                    data[j + 1] = val;
                    Draw();
                }
                else flag = 1;
            }
        }
    }

    protected void OnButtonClicked(object sender, EventArgs e)
    {

        Random rnd = new Random();
        for(int i = 0; i < data.Length; i++)
        {
            data[i] = rnd.Next(300);
        }

        Draw();

    }

    protected void OnDrawingareaExposeEvent(object o, ExposeEventArgs args)
    {
        area = (DrawingArea)o;

        Draw();

    }

    protected void OnHscaleValueChanged(object sender, EventArgs e)
    {
        data = new int[(int)hscale.Value];

        Random rnd = new Random();
        for (int i = 0; i < data.Length; i++)
        {
            data[i] = rnd.Next(300);
        }

        Draw();

    }

    protected void OnButton1Clicked(object sender, EventArgs e)
    {
        switch (combobox1.ActiveText)
        {
            case "Buble":
                Buble();
                break;
            case "Insertion":
                Insertion();
                break;
            case "Quick":
                quickSort(data, 0, data.Length - 1);
                Draw();
                break;
        }
    }

}
