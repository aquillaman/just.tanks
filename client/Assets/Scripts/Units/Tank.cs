using System;
using System.Collections.Generic;
using Pooling;
using Settings;
using UnityEngine;
using Behaviour = Behaviours.Behaviour;

namespace Units
{
    public class Tank : MonoBehaviour, ITarget, IPoolItem
    {
        public int HP = 17;
        protected Rigidbody _rigidbody;
        [SerializeField]
        private Transform _launchPoint;
        public Transform Turret;

        public int AimRadius => _settings.AimRadius;
        public int FireRadius  => _settings.FireRadius;
        public int CollisionDamage  => _settings.CollisionDamage;

        public ITarget Target;
    
        public virtual int LayerMask { get; }
        public event Action<Tank> Destroyed;
        public virtual Transform Transform { get; }
    
        private readonly List<Weapon> _weapons = new List<Weapon>();
        public Weapon CurrentWeapon;
        private int _weaponIndex = -1;
        private TankSettings _settings;
        protected bool _isDestroyed;

        private readonly List<Behaviour> Behaviours = new List<Behaviour>();

        protected void AddBehaviour(Behaviour behaviour)
        {
            Behaviours.Add(behaviour);
        }
        
        protected void ClearBehaviours()
        {
            Behaviours.Clear();
        }

        private void Update()
        {
            for (int i = 0; i < Behaviours.Count; i++)
            {
                Behaviours[i].Update();
            }
        }
    
        private void FixedUpdate()
        {
            for (int i = 0; i < Behaviours.Count; i++)
            {
                Behaviours[i].FixedUpdate();
            }
        }
    
        protected void ChangeWeapon()
        {
            if (_weapons.Count < 1)
            {
                Debug.LogError("No weapon assigned.");
                return;
            }

            CurrentWeapon = _weapons[++_weaponIndex % _weapons.Count];
        }

        private void SetWeapons(WeaponSettings[] weapons)
        {
            foreach (var weapon in weapons)
            {
                _weapons.Add(new Weapon(weapon, _launchPoint));
            }

            ChangeWeapon();
        }

        public void TakeDamage(int amount)
        {
            Debug.Log($"TakeDamage HP:{HP}, amount:{amount}");

            if ((HP -= amount) <= 0 && !_isDestroyed)
            {
                _isDestroyed = true;
                Destroyed?.Invoke(this);
                Reset();
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.rigidbody)
            {
                Debug.Log($"AddForce impulse:{collision.impulse}");
                _rigidbody.AddForce(-collision.impulse, ForceMode.Impulse);
                
                var target = collision.gameObject.GetComponentInParent<ITarget>();
                target?.TakeDamage(CollisionDamage);
            }
        }

        public void Configure(TankSettings settings)
        {
            _settings = settings;

            HP = settings.Helth;
            SetWeapons(settings.Weapons);
        }

        public virtual void Setup()
        {
            HP = _settings.Helth;
            _isDestroyed = false;
        }

        public virtual void Reset()
        {
            ClearBehaviours();
        }
    }
}