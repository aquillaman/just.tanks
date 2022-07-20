using PlayerInput;
using Pooling;
using Units;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoSingleton<GameManager>
{
    private PlayerController _controller;

    private void Start()
    {
        StartGame();
    }

    public void StartGame()
    {
        var tank = CreateAndSetupPlayerTank();
        CreateCameraController(tank.Transform);
    }

    private static PlayerTank CreateAndSetupPlayerTank()
    {
        var input = GetInput();
        var playerTank = ObjectFactory.Create<PlayerTank>();
        var playerController = playerTank.gameObject.AddComponent<PlayerController>();
        playerController.SetController(input);
        playerTank.Initialize();

        return playerTank;
    }
    
    private static IAgentInput GetInput()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        return FindObjectOfType<PlayerMobileInput>();
#else
        return FindObjectOfType<PlayerStandaloneInput>();
#endif
    }

    private static void CreateCameraController(Transform target)
    {
        var camera = FindObjectOfType<Camera>();
        var controller = camera.gameObject.AddComponent<FollowController>();
        controller.Target = target;
    }
}