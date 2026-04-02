using System;
using System.Collections.Generic;
using Fusion;
using Fusion.Sockets;
using R3;

namespace _VampireSurvivors.CodeBase.Services.Network
{
    public class FusionCallbacks : INetworkRunnerCallbacks
    {
        public readonly Subject<PlayerRef> PlayerJoined = new();
        public readonly Subject<PlayerRef> PlayerLeft = new();
        public readonly Subject<NetworkInput> InputReceived = new();
        public readonly Subject<Unit> Disconnected = new();
        public readonly Subject<Unit> Shutdown = new();

        public void OnObjectExitAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player)
        {
        }

        public void OnObjectEnterAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player)
        {
        }

        public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
        {
            PlayerJoined.OnNext(player);
        }

        public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
        {
            PlayerLeft.OnNext(player);
        }

        public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
        {
            Shutdown.OnNext(Unit.Default);
        }

        public void OnDisconnectedFromServer(NetworkRunner runner, NetDisconnectReason reason)
        {
            Disconnected.OnNext(Unit.Default);
        }

        public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token)
        {
        }

        public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason)
        {
        }

        public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message)
        {
        }

        public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ReliableKey key, ArraySegment<byte> data)
        {
        }

        public void OnReliableDataProgress(NetworkRunner runner, PlayerRef player, ReliableKey key, float progress)
        {
        }

        public void OnInput(NetworkRunner runner, NetworkInput input)
        {
            InputReceived.OnNext(input);
        }

        public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input)
        {
        }

        public void OnConnectedToServer(NetworkRunner runner)
        {
        }

        public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
        {
        }

        public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data)
        {
        }

        public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken)
        {
        }

        public void OnSceneLoadDone(NetworkRunner runner)
        {
        }

        public void OnSceneLoadStart(NetworkRunner runner)
        {
        }
    }
}
