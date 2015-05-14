using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Core.Components;
using Microsoft.Xna.Framework.Content;
namespace Core
{
	[Serializable]
	public class GameObject
	{
		
		private string Name;
		private Transform mtransform = new Transform(); 
		private List<Component> components = new List<Component>();
		private List<GameObject> children = new List<GameObject> ();



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

		public GameObject Parent {
			get {
				return parent;
			}
			set{
				parent = value;
			}
		}



		public Transform Transform {
			get {
				return transform;
			}
		}

		public GameObject ()
		{
			//Register all components 
			transform.RegisterComponent(this);
			foreach (Component c in components) {
				c.RegisterComponent (this);
			}
			foreach (GameObject g in children) {
				g.parent = this;
			}
			//Register to global gameobjects
			RegisterGameObject(this);
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

		public void Initialise(){
			transform.Initialize ();
			foreach (Component c in components) {
				c.Initialize ();
			}

		}

		public void AddComponent(Component component){
			this.components.Add (component);
		}

		public void Update(GameTime gameTime){
			transform.Update (gameTime);
			foreach (Component c in components) {
				if (c.Enabled)
					c.Update (gameTime);
			}

		}
		public void LoadContent(){
			transform.LoadContent ();
			foreach (Component c in components) {
				c.LoadContent ();
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
	}
}

