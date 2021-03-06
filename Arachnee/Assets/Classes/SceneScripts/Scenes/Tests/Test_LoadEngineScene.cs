﻿using System.Linq;
using Assets.Classes.Core.EntryProviders;
using Assets.Classes.Core.Models;
using Assets.Classes.CoreVisualization.ModelViewManagement;
using Assets.Classes.CoreVisualization.ModelViewManagement.Builders;
using Assets.Classes.CoreVisualization.ModelViews;
using Assets.Classes.CoreVisualization.PhysicsEngine;
using UnityEngine;

namespace Assets.Classes.SceneScripts.Scenes.Tests
{
    public class Test_LoadEngineScene : MonoBehaviour
    {
        public EntryView EntryViewPrefab;
        public ConnectionView ConnectionViewPrefab;
        public GraphEngine graphEngine;

        void Start ()
        {
            // sets up provider
            var sampleProvider = new MiniSampleProvider();

            var builder = new ModelViewBuilder();
            builder.SetPrefab<Movie>(EntryViewPrefab);
            builder.SetPrefab<Artist>(EntryViewPrefab);
            builder.SetPrefab(ConnectionViewPrefab);

            var provider = new ModelViewProvider(sampleProvider, builder);
            
            // load graph engine
            foreach (var entry in sampleProvider.Entries.Select(e => e.Id))
            {
                var entryView = provider.GetEntryView(entry);
                entryView.transform.position = Random.onUnitSphere*5;

                var entryViewRigidbody = entryView.GetComponent<Rigidbody>();
                if (entryViewRigidbody == null)
                {
                    Debug.LogError($"Rigidbody of {entryView.Entry} was null");
                    return;
                }
                
                foreach (var connectedEntryView in provider.GetAdjacentEntryViews(entryView, Connection.AllTypes()))
                {
                    var connectedRigidbody = connectedEntryView.GetComponent<Rigidbody>();
                    if (connectedRigidbody == null)
                    {
                        Debug.LogError("Rigidbody was null");
                        continue;
                    }

                    for (int i = 0; i < Random.value * 200; i++)
                    {
                        graphEngine.AddEdge(entryViewRigidbody, connectedRigidbody);
                    }
                }
            }
        }
    }
}
