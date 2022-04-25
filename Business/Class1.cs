using Core.Core.Aspects;
using Core.Models.Abstract;

namespace Business;

[CatchError]
public class Class1:IClass1
{
    public Class1()
    {
        
    }
    
    public void Denem()
    {
        throw new Exception("Deneme Hata");
    }
}

public interface IClass1
{

    void Denem();
}