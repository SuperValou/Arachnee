﻿using System;
using Assets.Classes.Core.Models;
using Assets.Classes.CoreVisualization.ModelViewManagement.Builders.ComponentInitializers;
using Assets.Classes.CoreVisualization.ModelViews;

namespace Assets.Classes.CoreVisualization.ModelViewManagement.Builders
{
    /// <summary>
    /// Class in charge of creating the Views corresponding to the given Models.
    /// </summary>
    public class ModelViewBuilder
    {
        private readonly EntryViewBuilder _entryViewBuilder = new EntryViewBuilder();
        private readonly ConnectionViewBuilder _connectionViewBuilder = new ConnectionViewBuilder();
        private readonly SearchResultViewBuilder _searchResultViewBuilder = new SearchResultViewBuilder();

        private IComponentInitializer _componentInitializer = new EmptyComponentInitializer();
        
        /// <summary>
        /// Sets the prefab that should be used to build an Entry.
        /// </summary>
        /// <typeparam name="TEntry">Type of Entry that should be represented by the given prefab.</typeparam>
        /// <param name="prefab">The prefab to set.</param>
        public void SetPrefab<TEntry>(EntryView prefab) where TEntry : Entry
        {
            _entryViewBuilder.EntryViewPrefabs[typeof(TEntry)] = prefab;
        }

        /// <summary>
        /// Sets the prefab that should be used to build a ConnectionView.
        /// </summary>
        /// <param name="prefab"></param>
        public void SetPrefab(ConnectionView prefab)
        {
            _connectionViewBuilder.ConnectionViewPrefab = prefab;
        }

        /// <summary>
        /// Sets the prefab that should be used to build a SearchResult.
        /// </summary>
        /// <param name="prefab"></param>
        public void SetPrefab(SearchResultView prefab)
        {
            _searchResultViewBuilder.SearchResultViewPrefab = prefab;
        }

        /// <summary>
        /// Sets the IComponentInitializer used by the builder when Views are being built.
        /// </summary>
        /// <param name="componentInitializer">The IComponentInitializer to use.</param>
        public void SetComponentInitializer(IComponentInitializer componentInitializer)
        {
            if (componentInitializer == null)
            {
                throw new ArgumentNullException(nameof(componentInitializer));
            }

            _componentInitializer = componentInitializer;
        }

        /// <summary>
        /// Build the EntryView corresponding to the given Entry. 
        /// Uses the prefab set by the SetPrefab method.
        /// </summary>
        /// <param name="entry">The Entry that should be represented by an EntryView.</param>
        /// <returns>The EntryView.</returns>
        public EntryView BuildView(Entry entry)
        {
            var view = _entryViewBuilder.BuildEntryView(entry);
            _componentInitializer.InitializeComponents(view);
            return view;
        }

        /// <summary>
        /// Build the ConnectionView corresponding to the connections existing between the given two EntryViews.
        /// Uses the prefab set by the SetPrefab method.
        /// </summary>
        /// <param name="leftEntryView">One of the two EntryView of the connection.</param>
        /// <param name="rightEntryView">The other EntryView of the connection.</param>
        /// <returns>The ConnectionView.</returns>
        public ConnectionView BuildView(EntryView leftEntryView, EntryView rightEntryView)
        {
            var view = _connectionViewBuilder.BuildConnectionView(leftEntryView, rightEntryView);
            _componentInitializer.InitializeComponents(view);
            return view;
        }

        /// <summary>
        /// Build the SearchResultView corresponding to the given SearchResult. 
        /// Uses the prefab set by the SetPrefab method.
        /// </summary>
        /// <param name="searchResult">The SearchResult that should be represented by a SearchResultView.</param>
        /// <returns>The SearchResultView.</returns>
        public SearchResultView BuildView(SearchResult searchResult)
        {
            var view = _searchResultViewBuilder.BuildResultView(searchResult);
            _componentInitializer.InitializeComponents(view);
            return view;
        }
    }
}