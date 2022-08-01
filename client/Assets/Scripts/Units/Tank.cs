using System;
using System.Collections.Generic;
using Pooling;
using Settings;
using UnityEngine;
using Behaviour = Behaviours.Behaviour;

namespace Units
{
    public class Tank : MonoBehaviour, ITarget, IPoolable
    {
        public int HP = 17;
        protected Rigidbody Rigidbody;
        [SerializeField]
        private Transform _launchPoint;
        public Transform Turret;

        public int AimRadius => _settings.AimRadius;
        public int FireRadius  => _settings.FireRadius;
        private int CollisionDamage  => _settings.CollisionDamage;

        public ITarget Target;
    
        public event Action<Tank> Destroyed;
        public event Action<IPoolable> ReturnToPool;

        private readonly List<Weapon> _weapons = new List<Weapon>();
        private int _weaponIndex = -1;
        private bool _isDestroyed;
        public Weapon CurrentWeapon;
        private TankSettings _settings;

        public Transform Transform { get; private set; }
        private readonly List<Behaviour> _behaviours = new List<Behaviour>();

        protected virtual void Awake()
        {
            Transform = GetComponent<Transform>();
            Rigidbody = GetComponent<Rigidbody>();

            Rigidbody.Sleep();
        }

        protected void AddBehaviour(Behaviour behaviour)
        {
            _behaviours.Add(behaviour);
        }

        private void ClearBehaviours()
        {
            _behaviours.Clear();
        }

        private void Update()
        {
            foreach (var behaviour in _behaviours) { behaviour.Update(); }
        }
    
        private void FixedUpdate()
        {
            foreach (var behaviour in _behaviours) { behaviour.FixedUpdate(); }
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
                ReturnToPool?.Invoke(this);
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.rigidbody)
            {
                Rigidbody.AddForce(-collision.impulse, ForceMode.Impulse);
                
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

        void IPoolable.Setup()
        {
            HP = _settings.Helth;
            _isDestroyed = false;
        }

        void IPoolable.Reset()
        {
            Rigidbody.Sleep();
            ClearBehaviours();
        }
    }
}