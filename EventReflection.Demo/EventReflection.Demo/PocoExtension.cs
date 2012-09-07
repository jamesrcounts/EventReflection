namespace EventReflection.Demo
{
    using System.ComponentModel;

    public class PocoExtension : Poco, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(sender, e);
            }
        }
    }
}