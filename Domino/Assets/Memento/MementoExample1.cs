//-------------------------------------------------------------------------------------
//	MediatorExample1.cs
//-------------------------------------------------------------------------------------

using UnityEngine;
using System.Collections;

//This real-world code demonstrates the Memento pattern which temporarily saves and then restores the SalesProspect's internal state.
    public class MementoExample1 : Singleton<MementoExample1>
    {
        SalesProspect s = new SalesProspect ( );
        ProspectMemory m = new ProspectMemory ( );
    void Start()
        {
         
       
            // Store internal state
          
            m.Memento = s.SaveMemento();
         
              //  s.RestoreMemento ( m.Memento );
        

        }

    public void SetData(Color data )
    {
        s.SetColor = data;
        m.Memento = s.SaveMemento ( );
    }

    public Color Restore ( )
    {
       return s.RestoreMemento ( m.Memento );
    }


    }



class Memento
{
    private Color setColor;


    // Constructor
    public Memento ( Color setColor )
    {
        this.setColor = setColor;
  ;

    }

    // Gets or sets name
    public Color SetColor
    {
        get { return setColor; }
        set { setColor = value; }
    }


}

class SalesProspect
{
    private Color setColor;
   

    // Gets or sets name
    public Color SetColor
    {
        get { return setColor; }
        set
        {
            setColor = value;
            Debug.Log ( "Name:  " + setColor );
        }
    }
    // Stores memento
    public Memento SaveMemento ( )
    {
        Debug.Log ( "\nSaving state --\n" );
        return new Memento ( setColor );
    }

    // Restores memento
    public Color RestoreMemento ( Memento memento )
    {
        Debug.Log ( "\nRestoring state --\n" );

        return memento.SetColor;

    }
}

class ProspectMemory
{
    private Memento _memento;

    // Property
    public Memento Memento
    {
        set { _memento = value; }
        get { return _memento; }
    }
}

