﻿using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VGDevs
{
	[CreateAssetMenu( menuName = VGDevs.kCreateMenuPrefixName + "Game/Item - Weapon")]
	public class WeaponData : BaseItemData
	{
		[Header("Weapon Config")]
		public Damage Damage;
		public List<Damage> ExtraDamages;
		public int Durability;
		
		[Tooltip("How many attacks PER SECOND")]
		public float AttackSpeed;
		
		public WeaponType WeaponType;
		public WeaponFlags WeaponFlags;
	}

	[System.Flags]
	public enum WeaponType
	{
		Melee, Ranged, Siege
	}

	[System.Flags]
	public enum WeaponFlags
	{
		MainHand, Offhand, OneHand, TwoHanded,
	}

	[Serializable]
	public class Damage
	{
		public int Min;
		public int Max;
		public DamageType Type;

		public int Value => GetCalculatedDamage();

		private int GetCalculatedDamage()
		{
			return UnityEngine.Random.Range(Min, Max)  *  VGDevs.Database.DamageModifiers.GetModByType(Type);
		}
	}

	[Serializable]
	public class DamageModifiers
	{
		public List<DamageModifier> Modifiers;

		public int GetModByType( DamageType type )
		{
			return Modifiers.Find((mod) => mod.Type == type).Modifier;
		}
	}
	
	[Serializable]
	public struct DamageModifier
	{
		public DamageType Type;
		public int Modifier;
	}

	[Serializable]
	public enum DamageType
	{
		None = 0,
		Physical,
		Magical,
		Piercing,
		Bludgeoning,
	}
}