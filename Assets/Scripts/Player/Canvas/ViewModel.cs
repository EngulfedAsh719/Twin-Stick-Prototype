using System.ComponentModel;
using UnityEngine;
using UnityWeld.Binding;

[Binding]
public class ViewModel : MonoBehaviour, INotifyPropertyChanged
{
    private string health = "100";

    public event PropertyChangedEventHandler PropertyChanged;

    [Binding]
    public string Health
    {
        get => health;
        set
        {
            if (health.Equals(value)) return;
            health = value;
            OnPropertyChanged("Health");
        }
    }

    private void OnPropertyChanged(string propertyName)
    {
        if (PropertyChanged != null)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
