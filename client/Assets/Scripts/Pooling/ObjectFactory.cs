using System;
using System.Collections.Generic;
using Projectiles;
using Settings;
using Units;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Pooling
{
    public static class ObjectFactory
    {
        delegate MonoBehaviour CreateEnemyDelegate();

        private static readonly Dictionary<Type, CreateEnemyDelegate> Map = new Dictionary<Type, CreateEnemyDelegate>()
        {
            { typeof(EnemyTank), CreateEnemyTank },
            { typeof(PlayerTank), CreatePlayerTank },
            { typeof(RoundShot), CreateRoundShot },
            { typeof(CanisterShot), CreateCanisterShot },
        };

        private static MonoBehaviour CreateRoundShot()
        {
            var asset = Resources.Load<RoundShot>("Prefabs/RoundShot");
            var instance = Object.Instantiate(asset, Vector3.zero, Quaternion.identity);
            return instance;
        }
    
        private static MonoBehaviour CreateCanisterShot()
        {
            var asset = Resources.Load<CanisterShot>("Prefabs/CanisterShot");
            var instance = Object.Instantiate(asset, Vector3.zero, Quaternion.identity);
            return instance;
        }

        public static T Create<T>() where T : MonoBehaviour
        {
            var type = typeof(T);
            if (!Map.TryGetValue(type, out var method))
            {
                Debug.LogError($"Type {type} not registered.");
            }

            return (T)method?.Invoke();
        }

        private static MonoBehaviour CreatePlayerTank()
        {
            const string settings = "Settings/Tank/PlayerTank";
            return CreateTank(settings);
        }

        private static MonoBehaviour CreateEnemyTank()
        {
            const string settings = "Settings/Tank/EnemyTank";
            return CreateTank(settings);
        }

        private static MonoBehaviour CreateTank(string settingsPath)
        {
            var asset = Resources.Load<TankSettings>(settingsPath);
            var instance = Object.Instantiate(asset.Prefab, Vector3.zero, Quaternion.identity);
            instance.Configure(asset);
            return instance;
        }
    }
}