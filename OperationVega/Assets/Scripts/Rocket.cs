﻿
namespace Assets.Scripts
{
	using System.Collections.Generic;
	using System.Linq;

	using Assets.Scripts.BaseClasses;
	using Assets.Scripts.Interfaces;

	using UnityEngine;

	/// <summary>
	/// The rocket class.
	/// </summary>
	public class Rocket : MonoBehaviour
	{
		/// <summary>
		/// The all parts.
		/// </summary>
		[SerializeField]
		private List<IRocketParts> allParts;

		/// <summary>
		/// The total quality.
		/// </summary>
		private uint totalQuality;

		/// <summary>
		/// Place holding value that represents the current cockpit in the list.
		/// Used for when the existing cockpit is being replaced and removing it from the list.
		/// </summary>
		private BaseCockpit currentCockpit;

		/// <summary>
		/// Place holding value that represents the current thrusters in the list.
		/// Used for when the existing thrusters is being replaced and removing it from the list.
		/// </summary>
		private BaseThrusters currentThrusters;

		/// <summary>
		/// Place holding value that represents the current wings in the list.
		/// Used for when the existing wings is being replaced and removing it from the list.
		/// </summary>
		private BaseWings currentWings;

		/// <summary>
		/// The cockpit 1.
		/// </summary>
		private Cockpit Cockpit1;

		private Cockpit Cockpit2;

		/// <summary>
		/// The cockpit 2.
		/// </summary>
		public GameObject testPrefab2;

		/// <summary>
		/// The cockpit 3.
		/// </summary>
		public Cockpit Cockpit3;

		/// <summary>
		/// The thrusters 1.
		/// </summary>
		public Thrusters Thrusters1;

		/// <summary>
		/// The thrusters 2.
		/// </summary>
		public Thrusters Thrusters2;

		/// <summary>
		/// The thrusters 3.
		/// </summary>
		public Thrusters Thrusters3;

		/// <summary>
		/// The wings 1.
		/// </summary>
		public Wings Wings1;

		/// <summary>
		/// The wings 2.
		/// </summary>
		public Wings Wings2;

		/// <summary>
		/// The wings 3.
		/// </summary>
		public Wings Wings3;

		public GameObject testPrefab;

		/// <summary>
		/// Gets or sets the total quality.
		/// </summary>
		public uint FullQuality
		{
			get
			{
				return this.totalQuality;
			}

			set
			{
				this.totalQuality = value;
			}
		}

		/// <summary>
		/// The ship build.
		/// </summary>
		/// <returns>
		/// The <see cref="bool"/>.
		/// </returns>
		public bool ShipBuild()
		{
			return this.allParts.OfType<BaseCockpit>().Any() &&
			       this.allParts.OfType<BaseThrusters>().Any() &&
			       this.allParts.OfType<BaseWings>().Any();
		}

		/// <summary>
		/// Function for adding and replacing parts in a list.
		/// Step 1) Checks the integer values that refer to the player's current amount of steel and fuel against the integer values of the object that represent its cost
		/// Step 2) Checks the type of the selected object that uses the interface IRocketParts.
		/// Step 3-1) Checks if an object of that type doesn't exists at any index of the list.
		/// Step 4) If Step 3-1 returns true:
		/// Adds the object to the list
		/// Increments the Rocket class's integer value total quality based on the object's integer value quality
		/// Decrements the values of the player's steel and fuel based on the object's steel and fuel cost values
		/// Sets an object to be equal to the selected object as current
		/// Exits the function
		/// Step 3-2) Else if Step 3-1 returns false, then check if an object of the same type exists in the list at any index and if the list doesn't contain the selected object
		/// Step 5) If Step 3-2 returns true:
		/// Decrement the Rocket's total quality based on the quality value the current object in the list,
		/// Remove the current object from the list,
		/// And repeat from the start until it meets a condition to exit the function
		/// </summary>
		/// <param name="selectedPart">
		/// The selected part.
		/// The object that the player is attempting to add to the list that will be checked.
		/// </param>
		public void AddPart(IRocketParts selectedPart)
		{
			var spareList = this.allParts.ToList();

			if (User.SteelCount >= selectedPart.SteelCost && User.FuelCount >= selectedPart.FuelCost)
			{
				if (selectedPart is BaseCockpit)
				{
					if (!spareList.OfType<BaseCockpit>().Any())
					{
						this.allParts.Add(selectedPart);
						this.totalQuality += selectedPart.Quality;
						User.SteelCount -= selectedPart.SteelCost;
						User.FuelCount -= selectedPart.FuelCost;
						this.currentCockpit = selectedPart as BaseCockpit;
					}
					else if (spareList.OfType<BaseCockpit>().Any() && !spareList.Contains(selectedPart))
					{
						this.totalQuality -= this.currentCockpit.Quality;
						this.allParts.Remove(this.currentCockpit);
						this.AddPart(selectedPart);
					}
				}
				else if (selectedPart is BaseThrusters)
				{
					if (!spareList.OfType<BaseThrusters>().Any())
					{
						this.allParts.Add(selectedPart);
						this.totalQuality += selectedPart.Quality;
						User.SteelCount -= selectedPart.SteelCost;
						User.FuelCount -= selectedPart.FuelCost;
						this.currentThrusters = selectedPart as BaseThrusters;
					}
					else if (spareList.OfType<BaseThrusters>().Any() && !spareList.Contains(selectedPart))
					{
						this.totalQuality -= this.currentThrusters.Quality;
						this.allParts.Remove(this.currentThrusters);
						this.AddPart(selectedPart);
					}
				}
				else if (selectedPart is BaseWings)
				{
					if (!spareList.OfType<BaseWings>().Any())
					{
						this.allParts.Add(selectedPart);
						this.totalQuality += selectedPart.Quality;
						User.SteelCount -= selectedPart.SteelCost;
						User.FuelCount -= selectedPart.FuelCost;
						this.currentWings = selectedPart as BaseWings;
					}
					else if (spareList.OfType<BaseWings>().Any() && !spareList.Contains(selectedPart))
					{
						this.totalQuality -= this.currentWings.Quality;
						this.allParts.Remove(this.currentWings);
						this.AddPart(selectedPart);
					}
				}
			}
		}

		/// <summary>
		/// Use this for initialization
		/// </summary>
		private void Start()
		{
			this.allParts = new List<IRocketParts>();

			this.Cockpit3.Accessed = new BaseCockpit(40, 200, 0, 80);

			this.Thrusters1.Accessed = new BaseThrusters(200, 50, 20);
			this.Thrusters2.Accessed = new BaseThrusters(200, 50, 50);
			this.Thrusters3.Accessed = new BaseThrusters(200, 50, 80);

			this.Wings1.Accessed = new BaseWings(200, 0, 20);
			this.Wings2.Accessed = new BaseWings(200, 0, 50);
			this.Wings3.Accessed = new BaseWings(200, 0, 80);
		}

		/// <summary>
		/// Update is called once per frame
		/// </summary>
		private void Update()
		{
			if (Input.GetMouseButtonDown(0))
			{
				this.Cockpit1 = MakeCockpit(new BaseCockpit(20, 200, 0, 20), testPrefab);
			}

			if (Input.GetMouseButtonDown(1))
			{
				this.Cockpit2 = MakeCockpit(new BaseCockpit(30, 200, 0, 50), testPrefab2);
			}

			if (Input.GetKeyDown(KeyCode.Keypad0))
			{
				this.AddPart(this.Cockpit3.Accessed);
			}

			if (Input.GetKeyDown(KeyCode.Keypad1))
			{
				this.AddPart(this.Thrusters1.Accessed);
			}

			if (Input.GetKeyDown(KeyCode.Keypad2))
			{
				this.AddPart(this.Thrusters2.Accessed);
			}

			if (Input.GetKeyDown(KeyCode.Keypad3))
			{
				this.AddPart(this.Thrusters3.Accessed);
			}

			if (Input.GetKeyDown(KeyCode.Keypad4))
			{
				this.AddPart(this.Wings1.Accessed);
			}

			if (Input.GetKeyDown(KeyCode.Keypad5))
			{
				this.AddPart(this.Wings2.Accessed);
			}

			if (Input.GetKeyDown(KeyCode.Keypad6))
			{
				this.AddPart(this.Wings3.Accessed);
			}

			if (Input.GetKeyDown(KeyCode.V))
			{
				User.SteelCount += 100;
			}

			if (Input.GetKeyDown(KeyCode.B))
			{
				User.FuelCount += 100;
			}

			Debug.Log(this.allParts.Count);
			Debug.Log("Total: " + this.totalQuality);
			Debug.Log(this.ShipBuild());
		}
		[SerializeField]
		private List<GameObject> rocketPart = new List<GameObject>();

		private Cockpit MakeCockpit(BaseCockpit c, GameObject n)
		{
			GameObject g = (GameObject)Instantiate(n) as GameObject;
			var behaviour = g.AddComponent<Cockpit>();
			behaviour.Create(c);
			g.name = "Cockpit";
			AddPart(c);
			return behaviour;
		}

		private GameObject MakeWing(BaseWings w, List<GameObject> rocketParts)
		{
			GameObject g = new GameObject();
			var behaviour = g.AddComponent<Wings>();
			behaviour.Create(w);
			g.name = "Wing::" + rocketParts.Count.ToString();
			rocketParts.Add(g);
			return g;
		}
		public void MakeWing(BaseWings w)
		{
			AddPart(w);
			currentWings = w;
			MakeWing(w, rocketPart);
		}

		private GameObject MakeThruster(BaseThrusters t, List<GameObject> rocketParts)
		{
			GameObject g = new GameObject();
			var behaviour = g.AddComponent<Thrusters>();
			behaviour.Create(t);
			g.name = "Thruster::" + rocketParts.Count.ToString();
			rocketParts.Add(g);
			return g;
		}
		public void MakeThruster(BaseThrusters t)
		{
			AddPart(t);
			currentThrusters = t;
			MakeThruster(t, rocketPart);
		}
	}
}
