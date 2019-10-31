using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PlaceDomino : EditorWindow{

    public enum ColorType
    {
        Single,
        Gradient
    }
    public enum LinkColorType
    {
        AutoGradient,
        Single,
        Gradient
    }
    public enum ConnectorDirection
    {
        Left, Right,
        Up, Down
    }
    Vector2 scrollPos;
    // Line
    public GameObject StartFromThisDomino;
    public Vector3 Direction;
    public float LineLength;
    public float Distance;
    public GameObject DominoPrefab;
    public string DominosName = "Line";
    public Color DominoColor = Color.white;
    public Color DominoColor2 = Color.white;
    public bool Parent = false;
    public string ParentName = "LineParent";
    public ColorType DominoColorType = ColorType.Single;

    //Rotate
    public GameObject Rotate_StartFromThisDomino;
    public float Rotate_Angle;
    public float Rotate_Distance;
    public int Rotate_NumberOfDominos;
    public GameObject Rotate_DominoPrefab;
    public Vector3 Rotate_PlaceDirection;
    public Vector3 Rotate_RotateByAxis;
    public string Rotate_DominosName = "Rotate";
    public Color Rotate_DominoColor = Color.white;
    public Color Rotate_DominoColor2 = Color.white;
    public bool Rotate_Parent = false;
    public string Rotate_ParentName = "RotateParent";
    public ColorType Rotate_DominoColorType = ColorType.Single;

    //Picture
    public Texture2D Picture1;
    public float DominoDistance = 2;
    public float LineDistance = 3;
    public GameObject StartPosition;
    public GameObject Pic_DominoPrefab;
    public string Pic_DominosName = "Picture";
    public bool Pic_Parent = true;
    public Vector3 DominoRotationVector;
    public string Pic_ParentName = "PictureParent";
    public Vector3 PlaceByAxis;
    public Color ConectorColor = Color.black;
    public Color ConectorColor2 = Color.black;
    public ColorType Conector_DominoColorType = ColorType.Single;
    public ConnectorDirection StartFrom = ConnectorDirection.Left;
    public bool ChangeDirection = false;

    //Connect
    public GameObject Start;
    public GameObject Destination;
    public float ConnectDistance = 2;
    public LinkColorType ConnectColorType = LinkColorType.AutoGradient;
    public Color LinkColor = Color.black;
    public Color LinkColor2 = Color.black;
    public GameObject Con_DominoPrefab;
    public bool Con_Parent = true;
    public string Con_DominosName = "Connect";
    public string Con_ParentName = "ConnectParent";

    [MenuItem("My Tools/Dominos Tools")]
    static void Init()
    {
        PlaceDomino window = (PlaceDomino)EditorWindow.GetWindow(typeof(PlaceDomino), false, "Dominos Tools");
        window.Show();
    }

    void OnGUI()
    {
        Rect tmp_rect = EditorGUILayout.BeginHorizontal();
        scrollPos = EditorGUILayout.BeginScrollView(scrollPos, GUILayout.Width(tmp_rect.width), GUILayout.Height(tmp_rect.height));
        GUILayout.Label("Place By Line", EditorStyles.boldLabel);
        StartFromThisDomino = EditorGUILayout.ObjectField("Start From This Domino", StartFromThisDomino, typeof(GameObject), true) as GameObject;
        Direction = EditorGUILayout.Vector3Field("Direction", Direction);
        LineLength = EditorGUILayout.FloatField("Length of Line", LineLength);
        Distance = EditorGUILayout.FloatField("Distance between Dominos", Distance);
        DominoPrefab = EditorGUILayout.ObjectField("Domino Prefab", DominoPrefab, typeof(GameObject), true) as GameObject;
        DominosName = EditorGUILayout.TextField("Dominos Name", DominosName);
        DominoColorType = (ColorType)EditorGUILayout.EnumPopup("Dominos' Color Type", DominoColorType);
        if(DominoColorType == ColorType.Single)
            DominoColor = EditorGUILayout.ColorField("Dominos' Color", DominoColor);
        else
        {
            DominoColor = EditorGUILayout.ColorField("Dominos' Color 1", DominoColor);
            DominoColor2 = EditorGUILayout.ColorField("Dominos' Color 2", DominoColor2);
        }
        Parent = EditorGUILayout.Toggle("Add a Object as Parent",Parent);
        if (Parent)
            ParentName = EditorGUILayout.TextField("Parent Name", ParentName);
        if (GUILayout.Button("Create!"))
        {
            CreateLine();
        }

        //Rotate
        GUILayout.Label("Place Dominos Rotate Around Circle", EditorStyles.boldLabel);
        Rotate_StartFromThisDomino = EditorGUILayout.ObjectField("Start From This Domino", Rotate_StartFromThisDomino, typeof(GameObject), true) as GameObject;
        Rotate_PlaceDirection = EditorGUILayout.Vector3Field("Place Direction", Rotate_PlaceDirection);
        Rotate_Distance = EditorGUILayout.FloatField("Distance between Dominos", Rotate_Distance);
        Rotate_RotateByAxis = EditorGUILayout.Vector3Field("Rotate By Axis", Rotate_RotateByAxis);
        Rotate_Angle = EditorGUILayout.FloatField("Rotate Angle", Rotate_Angle);
        Rotate_NumberOfDominos = EditorGUILayout.IntField("Number of Dominos", Rotate_NumberOfDominos);
        Rotate_DominoPrefab = EditorGUILayout.ObjectField("Domino Prefab", Rotate_DominoPrefab, typeof(GameObject), true) as GameObject;
        Rotate_DominosName = EditorGUILayout.TextField("Dominos Name", Rotate_DominosName);
        Rotate_DominoColorType = (ColorType)EditorGUILayout.EnumPopup("Dominos' Color Type", Rotate_DominoColorType);
        if (Rotate_DominoColorType == ColorType.Single)
            Rotate_DominoColor = EditorGUILayout.ColorField("Dominos' Color", Rotate_DominoColor);
        else
        {
            Rotate_DominoColor = EditorGUILayout.ColorField("Dominos' Color 1", Rotate_DominoColor);
            Rotate_DominoColor2 = EditorGUILayout.ColorField("Dominos' Color 2", Rotate_DominoColor2);
        }
        Rotate_Parent = EditorGUILayout.Toggle("Add a Object as Parent", Rotate_Parent);
        if (Rotate_Parent)
            Rotate_ParentName = EditorGUILayout.TextField("Parent Name", Rotate_ParentName);
        if (GUILayout.Button("Create!"))
        {
            CreateRotate();
        }

        //Picture
        GUILayout.Label("Place Dominos By Picture(1 pixel for 1 domino)(Strongly sugest use small picture)", EditorStyles.boldLabel);
        StartPosition = EditorGUILayout.ObjectField("Start Place Domino From", StartPosition, typeof(GameObject), true) as GameObject;
        Picture1 = EditorGUILayout.ObjectField("Use Which Pictrure?", Picture1, typeof(Texture2D), true) as Texture2D;
        LineDistance = EditorGUILayout.FloatField("Distance Between Line", LineDistance);
        DominoDistance = EditorGUILayout.FloatField("Distance Between Dominos", DominoDistance);
        PlaceByAxis = EditorGUILayout.Vector3Field("Place By Axis", PlaceByAxis);
        Pic_DominosName = EditorGUILayout.TextField("Dominos Name", Pic_DominosName);
        Pic_DominoPrefab = EditorGUILayout.ObjectField("Domino Prefab", Pic_DominoPrefab, typeof(GameObject), true) as GameObject;
        DominoRotationVector = EditorGUILayout.Vector3Field("Domino's Rotation(Degree)", DominoRotationVector);
        Pic_Parent = EditorGUILayout.Toggle("Add a Object as Parent", Pic_Parent);
        if (Pic_Parent)
            Pic_ParentName = EditorGUILayout.TextField("Parent Name", Pic_ParentName);
        Conector_DominoColorType = (ColorType)EditorGUILayout.EnumPopup("Conectors' Color Type", Conector_DominoColorType);
        if (Conector_DominoColorType == ColorType.Single)
            ConectorColor = EditorGUILayout.ColorField("Conectors' Color", ConectorColor);
        else
        {
            ConectorColor = EditorGUILayout.ColorField("Conectors' Color 1", ConectorColor);
            ConectorColor2 = EditorGUILayout.ColorField("Conectors' Color 2", ConectorColor2);
        }
        StartFrom = (ConnectorDirection)EditorGUILayout.EnumPopup("Start Place Connector From", StartFrom);
        ChangeDirection = EditorGUILayout.Toggle("Change Connector Direction", ChangeDirection);
        if (GUILayout.Button("Create!"))
        {
            CreatePicture();
        }

        //Connect
        GUILayout.Label("Connect Two Dominos", EditorStyles.boldLabel);
        Start = EditorGUILayout.ObjectField("Start From...", Start, typeof(GameObject), true) as GameObject;
        Destination = EditorGUILayout.ObjectField("Go to...", Destination, typeof(GameObject), true) as GameObject;
        ConnectDistance = EditorGUILayout.FloatField("Distance Between Dominos",ConnectDistance);
        ConnectColorType = (LinkColorType)EditorGUILayout.EnumPopup("Dominos' Color Type", ConnectColorType);
        if (ConnectColorType == LinkColorType.Single)
            LinkColor = EditorGUILayout.ColorField("Dominos' Color", LinkColor);
        else if(ConnectColorType == LinkColorType.Gradient)
        {
            LinkColor = EditorGUILayout.ColorField("Conectors' Color 1", LinkColor);
            LinkColor2 = EditorGUILayout.ColorField("Conectors' Color 2", LinkColor2);
        }
        Con_DominoPrefab = EditorGUILayout.ObjectField("Domino Prefab", Pic_DominoPrefab, typeof(GameObject), true) as GameObject;
        Con_DominosName = EditorGUILayout.TextField("Dominos Name", Con_DominosName);
        Con_Parent = EditorGUILayout.Toggle("Add a Object as Parent", Pic_Parent);
        if (Con_Parent)
            Con_ParentName = EditorGUILayout.TextField("Parent Name", Con_ParentName);
        if (GUILayout.Button("Create!"))
        {
            CreateLink();
        }

        EditorGUILayout.EndScrollView();
        EditorGUILayout.EndHorizontal();
    }

    private void CreateLine()
    {
        MeshRenderer renderer;
        MaterialPropertyBlock propertyBlock;

        GameObject new_Parent = null;
        if (Parent)
        {
            new_Parent = new GameObject();
            new_Parent.name = ParentName;
        }
        for (int i = 1; i * Distance < LineLength; i++)
        {
            GameObject new_obj = Instantiate(DominoPrefab, StartFromThisDomino.transform.position + Direction.normalized * Distance * i, StartFromThisDomino.transform.rotation);
            new_obj.name = DominosName + i;
            propertyBlock = new MaterialPropertyBlock ( );
            renderer = new_obj.GetComponent<MeshRenderer> ( );
            
           

            //MaterialPropertyBlock propBlock = new MaterialPropertyBlock();
            //var new_material = new Material(new_obj.GetComponent<MeshRenderer>().sharedMaterial);
            if (DominoColorType == ColorType.Single)
            {
                /*
                new_obj.GetComponent<MeshRenderer>().GetPropertyBlock(propBlock);
                propBlock.SetColor("_Color", DominoColor);
                new_obj.GetComponent<MeshRenderer>().SetPropertyBlock(propBlock);*/ //Property Block will clear color on play
              //  new_material.SetColor("_Color", DominoColor);//new way(use var)
                propertyBlock.SetColor ( "_Color", DominoColor );
                new_obj.GetComponent<Domino> ( ).Intermediate = DominoColor ;

            }
            else
                // new_material.SetColor("_Color", Color.Lerp(DominoColor, DominoColor2, Distance / LineLength * i));
                //new_obj.GetComponent<MeshRenderer>().material = new_material;
            propertyBlock.SetColor ( "_Color", Color.Lerp ( DominoColor, DominoColor2, Distance / LineLength * i ) );
            renderer.SetPropertyBlock ( propertyBlock );
            new_obj.GetComponent<Domino> ( ).Intermediate=  Color.Lerp ( DominoColor, DominoColor2, Distance / LineLength * i );
            if ((new_Parent != null) && Parent)
            {
                new_obj.transform.SetParent(new_Parent.transform);
            }
        }
    }

    private void CreateRotate()
    {

        MeshRenderer renderer;
        MaterialPropertyBlock propertyBlock;

        GameObject new_Parent = null;
        if (Rotate_Parent)
        {
            new_Parent = new GameObject();
            new_Parent.name = Rotate_ParentName;
        }
        Vector3 old_pos = Rotate_StartFromThisDomino.transform.position;
        for (int i = 1; i <= Rotate_NumberOfDominos; i++)
        {
            Vector3 new_pos;
            new_pos = Quaternion.AngleAxis(Rotate_Angle / Rotate_NumberOfDominos * i, Rotate_RotateByAxis) * Rotate_PlaceDirection.normalized * Rotate_Distance;
            Debug.Log(i/Rotate_NumberOfDominos);
            GameObject new_obj = Instantiate(Rotate_DominoPrefab, old_pos + new_pos, Rotate_StartFromThisDomino.transform.rotation * Quaternion.AngleAxis(Rotate_Angle / Rotate_NumberOfDominos * i, Rotate_RotateByAxis));
            new_obj.name = Rotate_DominosName + i;

            propertyBlock = new MaterialPropertyBlock ( );
            renderer = new_obj.GetComponent<MeshRenderer> ( );

           // var new_material = new Material(new_obj.GetComponent<MeshRenderer>().sharedMaterial);
            if(Rotate_DominoColorType == ColorType.Single)
            {

                propertyBlock.SetColor ( "_Color", Rotate_DominoColor );
                new_obj.GetComponent<Domino> ( ).Intermediate = Rotate_DominoColor;
            }
            else
            propertyBlock.SetColor ( "_Color", Color.Lerp ( Rotate_DominoColor, Rotate_DominoColor2, (float)i / (float)Rotate_NumberOfDominos ) );
            renderer.SetPropertyBlock ( propertyBlock );
            new_obj.GetComponent<Domino> ( ).Intermediate = Color.Lerp ( Rotate_DominoColor, Rotate_DominoColor2, (float)i / (float)Rotate_NumberOfDominos );
            //new_material.SetColor("_Color", Color.Lerp(Rotate_DominoColor, Rotate_DominoColor2, (float)i/(float)Rotate_NumberOfDominos ));
            //new_obj.GetComponent<MeshRenderer>().material = new_material;
            old_pos = new_obj.transform.position;
            if ((new_Parent != null) && Rotate_Parent)
                new_obj.transform.SetParent(new_Parent.transform);
        }
    }
    private void CreateLink()
    {
        MeshRenderer renderer;
        MaterialPropertyBlock propertyBlock;

        float Dist = Vector3.Distance(Start.transform.position, Destination.transform.position);
        GameObject new_Parent = null;
        if (Con_Parent)
        {
            new_Parent = new GameObject();
            new_Parent.name = Con_ParentName;
        }
        for (int i = 1; i * ConnectDistance < Dist; i++)
        {
            GameObject new_obj = Instantiate(DominoPrefab, Vector3.Lerp(Start.transform.position, Destination.transform.position, i*ConnectDistance / Dist ), Quaternion.Lerp(Start.transform.rotation, Destination.transform.rotation, i * ConnectDistance / Dist));
            new_obj.name = Con_DominosName + i;
            propertyBlock = new MaterialPropertyBlock ( );
            renderer = new_obj.GetComponent<MeshRenderer> ( );
            propertyBlock.SetColor ( "_Color", Rotate_DominoColor );
            new_obj.GetComponent<Domino> ( ).Intermediate = Rotate_DominoColor;
            //var new_material = new Material(new_obj.GetComponent<MeshRenderer>().sharedMaterial);
            Color materialA = Start.GetComponent<Domino> ( ).Intermediate;
            Color materialB = Destination.GetComponent<Domino> ( ).Intermediate;

            if (ConnectColorType == LinkColorType.Single)
            {
                propertyBlock.SetColor ( "_Color", LinkColor );
                new_obj.GetComponent<Domino> ( ).Intermediate = LinkColor;
            }
            else if(ConnectColorType == LinkColorType.Gradient)
            {

                propertyBlock.SetColor ( "_Color", Color.Lerp ( LinkColor, LinkColor2, i * ConnectDistance / Dist ) );
                new_obj.GetComponent<Domino> ( ).Intermediate = Color.Lerp ( LinkColor, LinkColor2, i * ConnectDistance / Dist );
            }
            else
                propertyBlock.SetColor ( "_Color", Color.Lerp ( materialA, materialB, i * ConnectDistance / Dist ) );
            new_obj.GetComponent<Domino> ( ).Intermediate = Color.Lerp ( materialA, materialB, i * ConnectDistance / Dist );
            renderer.SetPropertyBlock ( propertyBlock );
            if ((new_Parent != null) && Con_Parent)
            {
                new_obj.transform.SetParent(new_Parent.transform);
            }
        }

    }
    private void CreatePicture()
    {
        Color[] pix = Picture1.GetPixels(0, 0, Picture1.width, Picture1.height);
        GameObject new_Parent = null;
        GameObject Domino_Parent = null;
        GameObject Connector_Parent = null;
        if (Pic_Parent)
        {
            new_Parent = new GameObject(Pic_ParentName);
            Domino_Parent = new GameObject("Dominos");
            Domino_Parent.transform.SetParent(new_Parent.transform);
            Connector_Parent = new GameObject("Connectors");
            Connector_Parent.transform.SetParent(new_Parent.transform);
        }
        if (StartFrom == ConnectorDirection.Left || StartFrom == ConnectorDirection.Right)
        {
            int small_fact, big_fact;
            if (StartFrom == ConnectorDirection.Left)
            {
                small_fact = -1;
                big_fact = 0;//Go throuth whole width
            }
            else
            {
                small_fact = 1;
                big_fact = 1;//Go throuth whole width
            }
            for (int i = 0; i <= (int)(Picture1.height / 5); i++)
            {
                for (int r = 0; r < 5; r++)//first layer
                {
                    Vector3 new_pos_1 = new Vector3(1 * small_fact * DominoDistance + big_fact * DominoDistance * (Picture1.width - 1), 0, r * LineDistance + i * 5f * LineDistance);
                    new_pos_1 = Quaternion.AngleAxis(90, PlaceByAxis) * new_pos_1;
                    GameObject new_obj_1 = Instantiate(Pic_DominoPrefab, StartPosition.transform.position + new_pos_1, Quaternion.Euler(DominoRotationVector) * Quaternion.AngleAxis(90, PlaceByAxis));
                    var new_material = new Material(new_obj_1.GetComponent<MeshRenderer>().sharedMaterial);
                    if(Conector_DominoColorType == ColorType.Single)
                        new_material.SetColor("_Color", ConectorColor);
                    else
                        new_material.SetColor("_Color", Color.Lerp(ConectorColor, ConectorColor2, (r * LineDistance + i * 5f * LineDistance) / (LineDistance * Picture1.height)));
                    new_obj_1.name = "Connector5" + r + "y" + i;
                    new_obj_1.GetComponent<MeshRenderer>().material = new_material;
                    if ((new_Parent != null) && Pic_Parent)
                        new_obj_1.transform.SetParent(Connector_Parent.transform);
                }
                for (int r = 0; r < 4; r++)//second layer
                {
                    Vector3 new_pos_1 = new Vector3(2 * small_fact * DominoDistance + big_fact * DominoDistance * (Picture1.width - 1), 0, (2f * r + 1f) / 2f * LineDistance + i * 5f * LineDistance);
                    new_pos_1 = Quaternion.AngleAxis(90, PlaceByAxis) * new_pos_1;
                    GameObject new_obj_1 = Instantiate(Pic_DominoPrefab, StartPosition.transform.position + new_pos_1, Quaternion.Euler(DominoRotationVector) * Quaternion.AngleAxis(90, PlaceByAxis));
                    var new_material = new Material(new_obj_1.GetComponent<MeshRenderer>().sharedMaterial);
                    if (Conector_DominoColorType == ColorType.Single)
                        new_material.SetColor("_Color", ConectorColor);
                    else
                        new_material.SetColor("_Color", Color.Lerp(ConectorColor, ConectorColor2, ((2f * r + 1f) / 2f * LineDistance + i * 5f * LineDistance) / (LineDistance * Picture1.height)));
                    new_obj_1.name = "Connector4" + r + "y" + i;
                    new_obj_1.GetComponent<MeshRenderer>().material = new_material;
                    if ((new_Parent != null) && Pic_Parent)
                        new_obj_1.transform.SetParent(Connector_Parent.transform);
                }
                for (int r = 0; r < 3; r++)//third layer
                {
                    Vector3 new_pos_1 = new Vector3(3 * small_fact * DominoDistance + big_fact * DominoDistance * (Picture1.width - 1), 0, (r + 1f) * LineDistance + i * 5f * LineDistance);
                    new_pos_1 = Quaternion.AngleAxis(90, PlaceByAxis) * new_pos_1;
                    GameObject new_obj_1 = Instantiate(Pic_DominoPrefab, StartPosition.transform.position + new_pos_1, Quaternion.Euler(DominoRotationVector) * Quaternion.AngleAxis(90, PlaceByAxis));
                    var new_material = new Material(new_obj_1.GetComponent<MeshRenderer>().sharedMaterial);
                    if (Conector_DominoColorType == ColorType.Single)
                        new_material.SetColor("_Color", ConectorColor);
                    else
                        new_material.SetColor("_Color", Color.Lerp(ConectorColor, ConectorColor2, ((r + 1f) * LineDistance + i * 5f * LineDistance) / (LineDistance * Picture1.height)));
                    new_obj_1.name = "Connector3" + r + "y" + i;
                    new_obj_1.GetComponent<MeshRenderer>().material = new_material;
                    if ((new_Parent != null) && Pic_Parent)
                        new_obj_1.transform.SetParent(Connector_Parent.transform);
                }
                for (int r = 0; r < 2; r++)//fourth layer
                {
                    Vector3 new_pos_1 = new Vector3(4 * small_fact * DominoDistance + big_fact * DominoDistance * (Picture1.width - 1), 0, (2f * r + 3f) / 2f * LineDistance + i * 5f * LineDistance);
                    new_pos_1 = Quaternion.AngleAxis(90, PlaceByAxis) * new_pos_1;
                    GameObject new_obj_1 = Instantiate(Pic_DominoPrefab, StartPosition.transform.position + new_pos_1, Quaternion.Euler(DominoRotationVector) * Quaternion.AngleAxis(90, PlaceByAxis));
                    var new_material = new Material(new_obj_1.GetComponent<MeshRenderer>().sharedMaterial);
                    if (Conector_DominoColorType == ColorType.Single)
                        new_material.SetColor("_Color", ConectorColor);
                    else
                        new_material.SetColor("_Color", Color.Lerp(ConectorColor, ConectorColor2, ((2f * r + 3f) / 2f * LineDistance + i * 5f * LineDistance) / (LineDistance * Picture1.height)));
                    new_obj_1.name = "Connector2" + r + "y" + i;
                    new_obj_1.GetComponent<MeshRenderer>().material = new_material;
                    if ((new_Parent != null) && Pic_Parent)
                        new_obj_1.transform.SetParent(Connector_Parent.transform);
                }
                for (int r = 0; r < 1; r++)//fifth layer
                {
                    Vector3 new_pos_1 = new Vector3(5 * small_fact * DominoDistance + big_fact * DominoDistance * (Picture1.width - 1), 0, (r + 2f) * LineDistance + i * 5f * LineDistance);
                    new_pos_1 = Quaternion.AngleAxis(90, PlaceByAxis) * new_pos_1;
                    GameObject new_obj_1 = Instantiate(Pic_DominoPrefab, StartPosition.transform.position + new_pos_1, Quaternion.Euler(DominoRotationVector) * Quaternion.AngleAxis(90, PlaceByAxis));
                    var new_material = new Material(new_obj_1.GetComponent<MeshRenderer>().sharedMaterial);
                    if (Conector_DominoColorType == ColorType.Single)
                        new_material.SetColor("_Color", ConectorColor);
                    else
                        new_material.SetColor("_Color", Color.Lerp(ConectorColor, ConectorColor2, ( (r + 2f) * LineDistance + i * 5f * LineDistance ) / (LineDistance * Picture1.height) ));
                    new_obj_1.name = "Connector1" + r + "y" + i;
                    new_obj_1.GetComponent<MeshRenderer>().material = new_material;
                    if ((new_Parent != null) && Pic_Parent)
                        new_obj_1.transform.SetParent(Connector_Parent.transform);
                }
                if(ChangeDirection)
                {
                    small_fact = -small_fact;
                    if (big_fact > 0)
                        big_fact = 0;
                    else
                        big_fact = 1;
                }
            }
        }
        else
        {
            int small_fact, big_fact;
            if (StartFrom == ConnectorDirection.Down)
            {
                small_fact = -1;
                big_fact = 0;//Go throuth whole width
            }
            else
            {
                small_fact = 1;
                big_fact = 1;//Go throuth whole width
            }
            for (int i = 0; i <= (int)(Picture1.width / 5); i++)
            {
                for (int r = 0; r < 5; r++)//first layer
                {
                    Vector3 new_pos_1 = new Vector3(r * DominoDistance + i * 5f * DominoDistance, 0, 1 * small_fact * LineDistance + big_fact * LineDistance * (Picture1.height - 1));
                    new_pos_1 = Quaternion.AngleAxis(90, PlaceByAxis) * new_pos_1;
                    GameObject new_obj_1 = Instantiate(Pic_DominoPrefab, StartPosition.transform.position + new_pos_1, Quaternion.Euler(DominoRotationVector) * Quaternion.AngleAxis(90, PlaceByAxis));
                    var new_material = new Material(new_obj_1.GetComponent<MeshRenderer>().sharedMaterial);
                    if (Conector_DominoColorType == ColorType.Single)
                        new_material.SetColor("_Color", ConectorColor);
                    else
                        new_material.SetColor("_Color", Color.Lerp(ConectorColor, ConectorColor2, (r * DominoDistance + i * 5f * DominoDistance) / (DominoDistance * Picture1.width) ));
                    new_obj_1.name = "Connector5" + r + "y" + i;
                    new_obj_1.GetComponent<MeshRenderer>().material = new_material;
                    if ((new_Parent != null) && Pic_Parent)
                        new_obj_1.transform.SetParent(Connector_Parent.transform);
                }
                for (int r = 0; r < 4; r++)//second layer
                {
                    Vector3 new_pos_1 = new Vector3((2f * r + 1f) / 2 * DominoDistance + i * 5f * DominoDistance, 0, 2 * small_fact * LineDistance + big_fact * LineDistance * (Picture1.height - 1));
                    new_pos_1 = Quaternion.AngleAxis(90, PlaceByAxis) * new_pos_1;
                    GameObject new_obj_1 = Instantiate(Pic_DominoPrefab, StartPosition.transform.position + new_pos_1, Quaternion.Euler(DominoRotationVector) * Quaternion.AngleAxis(90, PlaceByAxis));
                    var new_material = new Material(new_obj_1.GetComponent<MeshRenderer>().sharedMaterial);
                    if (Conector_DominoColorType == ColorType.Single)
                        new_material.SetColor("_Color", ConectorColor);
                    else
                        new_material.SetColor("_Color", Color.Lerp(ConectorColor, ConectorColor2, ((2f * r + 1f) / 2 * DominoDistance + i * 5f * DominoDistance) / (DominoDistance * Picture1.width) ));
                    new_obj_1.name = "Connector4" + r + "y" + i;
                    new_obj_1.GetComponent<MeshRenderer>().material = new_material;
                    if ((new_Parent != null) && Pic_Parent)
                        new_obj_1.transform.SetParent(Connector_Parent.transform);
                }
                for (int r = 0; r < 3; r++)//third layer
                {
                    Vector3 new_pos_1 = new Vector3((r + 1f) * DominoDistance + i * 5f * DominoDistance, 0, 3 * small_fact * LineDistance + big_fact * LineDistance * (Picture1.height - 1));
                    new_pos_1 = Quaternion.AngleAxis(90, PlaceByAxis) * new_pos_1;
                    GameObject new_obj_1 = Instantiate(Pic_DominoPrefab, StartPosition.transform.position + new_pos_1, Quaternion.Euler(DominoRotationVector) * Quaternion.AngleAxis(90, PlaceByAxis));
                    var new_material = new Material(new_obj_1.GetComponent<MeshRenderer>().sharedMaterial);
                    if (Conector_DominoColorType == ColorType.Single)
                        new_material.SetColor("_Color", ConectorColor);
                    else
                        new_material.SetColor("_Color", Color.Lerp(ConectorColor, ConectorColor2, ((r + 1f) * DominoDistance + i * 5f * DominoDistance) / (DominoDistance * Picture1.width) ));
                    new_obj_1.name = "Connector3" + r + "y" + i;
                    new_obj_1.GetComponent<MeshRenderer>().material = new_material;
                    if ((new_Parent != null) && Pic_Parent)
                        new_obj_1.transform.SetParent(Connector_Parent.transform);
                }
                for (int r = 0; r < 2; r++)//fourth layer
                {
                    Vector3 new_pos_1 = new Vector3((2f * r + 3f) / 2f * DominoDistance + i * 5f * DominoDistance, 0, 4 * small_fact * LineDistance + big_fact * LineDistance * (Picture1.height - 1));
                    new_pos_1 = Quaternion.AngleAxis(90, PlaceByAxis) * new_pos_1;
                    GameObject new_obj_1 = Instantiate(Pic_DominoPrefab, StartPosition.transform.position + new_pos_1, Quaternion.Euler(DominoRotationVector) * Quaternion.AngleAxis(90, PlaceByAxis));
                    var new_material = new Material(new_obj_1.GetComponent<MeshRenderer>().sharedMaterial);
                    if (Conector_DominoColorType == ColorType.Single)
                        new_material.SetColor("_Color", ConectorColor);
                    else
                        new_material.SetColor("_Color", Color.Lerp(ConectorColor, ConectorColor2, ((2f * r + 3f) / 2f * DominoDistance + i * 5f * DominoDistance) / (DominoDistance * Picture1.width) ));
                    new_obj_1.name = "Connector2" + r + "y" + i;
                    new_obj_1.GetComponent<MeshRenderer>().material = new_material;
                    if ((new_Parent != null) && Pic_Parent)
                        new_obj_1.transform.SetParent(Connector_Parent.transform);
                }
                for (int r = 0; r < 1; r++)//fifth layer
                {
                    Vector3 new_pos_1 = new Vector3((r + 2f) * DominoDistance + i * 5f * DominoDistance, 0, 5 * small_fact * LineDistance + big_fact * LineDistance * (Picture1.height - 1));
                    new_pos_1 = Quaternion.AngleAxis(90, PlaceByAxis) * new_pos_1;
                    GameObject new_obj_1 = Instantiate(Pic_DominoPrefab, StartPosition.transform.position + new_pos_1, Quaternion.Euler(DominoRotationVector) * Quaternion.AngleAxis(90, PlaceByAxis));
                    var new_material = new Material(new_obj_1.GetComponent<MeshRenderer>().sharedMaterial);
                    if (Conector_DominoColorType == ColorType.Single)
                        new_material.SetColor("_Color", ConectorColor);
                    else
                        new_material.SetColor("_Color", Color.Lerp(ConectorColor, ConectorColor2, ((r + 2f) * DominoDistance + i * 5f * DominoDistance) / (DominoDistance * Picture1.width) ));
                    new_obj_1.name = "Connector1" + r + "y" + i;
                    new_obj_1.GetComponent<MeshRenderer>().material = new_material;
                    if ((new_Parent != null) && Pic_Parent)
                        new_obj_1.transform.SetParent(Connector_Parent.transform);
                }
                if (ChangeDirection)
                {
                    small_fact = -small_fact;
                    if (big_fact > 0)
                        big_fact = 0;
                    else
                        big_fact = 1;
                }
            }

        }
        for (int i = 0; i < Picture1.height; i++)//y
            for(int j = 0; j < Picture1.width; j++)//x
            {
                Vector3 new_pos = new Vector3(j*DominoDistance, 0, i * LineDistance);
                new_pos = Quaternion.AngleAxis(90, PlaceByAxis) * new_pos;
                GameObject new_obj = Instantiate(Pic_DominoPrefab, StartPosition.transform.position + new_pos, Quaternion.Euler(DominoRotationVector) *Quaternion.AngleAxis(90, PlaceByAxis));
                var new_material = new Material(new_obj.GetComponent<MeshRenderer>().sharedMaterial);
                new_material.SetColor("_Color", pix[i*Picture1.width + j]);
                new_obj.GetComponent<MeshRenderer>().material = new_material;
                new_obj.name = Pic_DominosName + i + j;
                if ((new_Parent != null) && Pic_Parent)
                    new_obj.transform.SetParent(Domino_Parent.transform);
            }
    }

}
