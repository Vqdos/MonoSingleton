# MonoSingleton
MonoBehaviour singleton (Unity)

##
Usage:

```c#
NetworkManager.Instance.Connect()
```

```c#
public class NetworkManager : MonoSingleton<NetworkManager>
{
    public string Status;
        
    protected override void Initialize()
    {
        Status = "Initialized";
    }
        
    public bool Connect()
    {
        return true;
    }
}
```
