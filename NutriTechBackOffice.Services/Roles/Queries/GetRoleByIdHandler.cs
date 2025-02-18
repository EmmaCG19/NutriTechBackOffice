﻿using NutriTechBackOffice.Domain.Entities;
using Google.Cloud.Firestore;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NutriTechBackOffice.Services.Roles.Queries
{
    class GetRoleByIdHandler : IRequestHandler<GetRoleByIdQuery, Role>
    {
        private CollectionReference rolesRef;
        private DocumentSnapshot existingRole;

        public GetRoleByIdHandler(FirestoreDb firestore)
        {
            rolesRef = firestore.Collection("Roles");
        }
        public async Task<Role> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
        {
            existingRole = await rolesRef.Document(request.Id.ToString()).GetSnapshotAsync();
            if (existingRole.Exists)
            {
                Dictionary<string, object> Role = existingRole.ToDictionary();
                string json = JsonConvert.SerializeObject(Role);
                Role newRole = JsonConvert.DeserializeObject<Role>(json);
                return newRole;
            }else
            {
                return new Role();
            }
        }
    }
}
