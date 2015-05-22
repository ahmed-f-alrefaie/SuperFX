using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using SuperEFEX.Core.Components;
using Microsoft.Xna.Framework.Content;
using SuperEFEX.Core.Content;
using SuperEFEX.Kernal.Utilities;
namespace SuperEFEX.Core
{
	[Serializable]
	public class GameObject
	{
		
		private string name="Object";
		private Transform mtransform = new Transform(); 
		private List<Component> components = new List<Component>();
		private List<GameObject> children = new List<GameObject> ();

		private int Priority;


		public string Name {
			get {
				return name;
			}
			set {
				name = value;
			}
		}


		private GameObject parent=null;
		private bool mEnabled = true;

		public bool Enabled{
			get{
				if(parent!=null){
					return parent.mEnabled & Enabled;
				}
				return mEnabled;
			}
		}

		public Transform transform {
			get {
				return mtransform;
			}
			set {
				mtransform = value;
			}
		}

		private static List<GameObject> mGameObjects = new List<GameObject> ();
		private static List<GameObject> mPrefabs = new List<GameObject> ();

		public GameObject FindGameObject(string name){
			foreach (GameObject g in mGameObjects) {
				if (g.Name == name) {
					return g;
				}

			}
			return null;
		}

		public GameObject Parent {
			get {
				return parent;
			}
			set{
				parent = value;
			}
		}

		public List<Component> Components{
			get{ return components; }
			set{ components = value; }
		}

		public GameObject ()
		{
			mGameObjects.Add (this);
			//Register all components 
			transform.RegisterComponent(this);
		}

		public static void RegisterGameObject(GameObject g){
			mGameObjects.Add(g);
		}

		public T GetComponent<T>()  where T : Component{
			for (int i = 0; i < components.Count; i++) {
				if (components[i].GetType() == typeof(T))
					return (T)components[i];
			}
			return null;

		}

		public void Start(){
			foreach (Component c in components) {
				c.RegisterComponent (this);
			}
			foreach (GameObject g in children) {
				g.parent = this;
			}
			
		}


		public static void StartUpGameObjects(){
		}

		protected static void AssignOrder(){
			foreach (GameObject g in mGameObjects) {
				g.GetOrder ();
			}

			//Sort them for updating
			mGameObjects.Sort((x,y)=>{return x.Priority.CompareTo(y.Priority);});
		}
		
		//Those with parents should be updated last
		private void GetOrder(){
			int order = 0;
			GameObject g = parent;
			while (g != null) {
				g = g.Parent;
				order++;
			}

			Priority = order;

		}

		public void Initialise(){
			transform.Initialize ();
			foreach (Component c in components) {
				c.Initialize ();
			}

		}

		public static void LoadGameContent(FXContent content){
			foreach (GameObject go in mGameObjects) {
			}

		}


		public void AddComponent(Component component){
			this.components.Add (component);
			component.RegisterComponent (this);

		}

		public void Update(GameTime gameTime){
			transform.Update (gameTime);
			foreach (Component c in components) {
				if (c.Enabled)
					c.Update (gameTime);
			}

		}
		public void LoadContent(FXContent content){
			transform.LoadContent (content);
			foreach (Component c in components) {
				c.LoadContent (content);
			}

		}
		public void FixedUpdate(GameTime gameTime){
			foreach (Component c in components) {
				if (c.Enabled)
					c.FixedUpdate (gameTime);
			}

		}
		public static void GameUpdate(GameTime gameTime){
			foreach (GameObject g in mGameObjects) {
				if (g.Enabled)
					g.Update (gameTime);
			}
		}

		public static void GameFixedUpdate(GameTime gameTime){
			foreach (GameObject g in mGameObjects) {
				if (g.Enabled)
					g.FixedUpdate (gameTime);
			}
		}

		protected GameObject Clone(FXContent content){

			GameObject go = new GameObject ();
			go.Name += "(Clone)";
			go.transform = (Transform)go.transform.Clone ();
			go.transform.RegisterComponent(this);
			foreach (Component c in components) {
				go.AddComponent (c.Clone ());
			}
			go.transform.Scale = go.transform.Scale;
			go.transform.EulerAngles = go.transform.EulerAngles;
			go.Start ();
			go.LoadContent (content);
			go.Initialise ();


			return go;


		}

		public static GameObject Clone(FXContent content,GameObject g,Vector3 position){

			GameObject go = g.Clone (content);
			go.transform.Position = position;

			return go;


		}

		public static GameObject Clone(FXContent content,string name){

			foreach (GameObject g in mGameObjects) {
				if (g.Name == name) {
					return GameObject.Clone (content,g,g.transform.Position);
				}
			}
			return null;


		}

	}
}

