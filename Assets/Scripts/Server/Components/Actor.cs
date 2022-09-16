using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Remote
{
   public struct Actor { }

   public struct Button
   {
      public Vector3 Position;
      public float Radius;
   }

   public struct Transform
   {
      public Vector3 Position;
   }

   public struct Movement
   {
      public float Speed;
   }

   public struct Destination
   {
      public Vector3 Position;
   }
}