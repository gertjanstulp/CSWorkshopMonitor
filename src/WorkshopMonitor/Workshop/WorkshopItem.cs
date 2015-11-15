﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorkshopMonitor
{
    /// <summary>
    /// Represents a class holding the data of a single workshop item used for showing workshop item information
    /// </summary>
    public class WorkshopItem
    {
        /// <summary>
        /// Occurs when the instance count of the entry has been updated
        /// </summary>
        public event EventHandler<EventArgs> InstanceCountUpdated;

        /// <summary>
        /// Initializes a new instance of the <see cref="WorkshopItem" /> class.
        /// </summary>
        /// <param name="workshopId">The identifier of the workshop item</param>
        /// <param name="readableName">The readable name of the building</param>
        /// <param name="technicalName">The technical name of the building</param>
        /// <param name="buildingType">Type of the building.</param>
        public WorkshopItem(ulong workshopId, string readableName, string technicalName, BuildingType buildingType)
        {
            WorkshopId = workshopId;
            ReadableName = readableName;
            TechnicalName = technicalName;
            BuildingType = buildingType;

            Id = Guid.NewGuid();
        }

        /// <summary>
        /// Gets or sets the identifier, used for internal reference
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets the workshop identifier.
        /// </summary>
        public ulong WorkshopId { get; private set; }

        /// <summary>
        /// Gets the readable name.
        /// </summary>
        public string ReadableName { get; private set; }

        /// <summary>
        /// Gets the name of the technical.
        /// </summary>
        public string TechnicalName { get; private set; }

        /// <summary>
        /// Gets the type of the building.
        /// </summary>
        public BuildingType BuildingType { get; private set; }

        /// <summary>
        /// Gets the number of building instances that are created from the workshop item
        /// </summary>
        public int InstanceCount { get; private set; }
        
        /// <summary>
        /// Gets the number of building instances that are created from the workshop item
        /// </summary>
        /// <param name="instanceCount">The number of building instances</param>
        public void SetInstanceCount(int instanceCount)
        {
            InstanceCount = instanceCount;
            OnInstanceCountUpdated();
        }

        /// <summary>
        /// Raises the InstanceCountUpdated event
        /// </summary>
        protected virtual void OnInstanceCountUpdated()
        {
            var handler = this.InstanceCountUpdated;
            if (handler != null)
                handler.Invoke(this, EventArgs.Empty);
        }
    }
}