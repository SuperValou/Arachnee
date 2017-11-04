﻿using System.Linq;
using Assets.Classes.Core.EntryProviders;
using Assets.Classes.Core.Models;
using Assets.Classes.CoreVisualization.ModelViewManagement;
using Assets.Classes.CoreVisualization.ModelViewManagement.Builders;
using Assets.Classes.CoreVisualization.ModelViews;
using Assets.Classes.CoreVisualization.PhysicsEngine;
using UnityEngine;

namespace Assets.Classes.SceneScripts.Tests
{
    public class Test_LoadEngineScene : MonoBehaviour
    {
        public EntryView EntryViewPrefab;
        public ConnectionView ConnectionViewPrefab;
        public GraphEngine graphEngine;

        void Start ()
        {
            // sets up manager
            var sampleProvider = new MiniSampleProvider();

            var builder = new ModelViewBuilder();
            builder.SetPrefab<Movie>(EntryViewPrefab);
            builder.SetPrefab<Artist>(EntryViewPrefab);
            builder.SetPrefab(ConnectionViewPrefab);

            var manager = new ModelViewManager(sampleProvider, builder);
            
            // load graph engine
            foreach (var entry in sampleProvider.Entries.Select(e => e.Id))
            {
                var entryView = manager.GetEntryView(entry);
                entryView.transform.position = Random.onUnitSphere*5;
            
                graphEngine.Add(entryView);

                foreach (var connectionView in manager.GetConnectionViews(entryView))
                {
                    graphEngine.Add(connectionView);
                }
            }
        }
    }
}
