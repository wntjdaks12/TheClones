using System;

public interface IPollingScrollview 
{
    public void Init(int index);

    public Action<int> ClickEvent { get; set; }
}
