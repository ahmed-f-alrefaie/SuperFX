﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
namespace Renderer
{

	public enum RenderType{
		RENDER_3D,
		BACKGROUND,
		MODE_7,
		FOREGROUND,
		HUD
	}

	public interface IRenderable
	{

		bool Enable{get;}
		RenderType RenderType{get;}
		//private 
	}

	public interface IWorld{

		Matrix WorldMatrix {
			get;
		}


	}


	public abstract class Renderable3D : IRenderable , IWorld
	{
		protected bool Enabled;
		protected Matrix world;
		public Matrix WorldMatrix{ get { return world; } }
		public abstract ModelContent.EdgeTable[] EdgeTables{ get;}
		//Add bounding sphere here
		public abstract void Transform3D();
		public RenderType RenderType{get { return RenderType.RENDER_3D; }}
		public bool Enable{get{ return Enabled; } set{ Enabled = value; }}
	}

}

