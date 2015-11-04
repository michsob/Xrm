using AutoMapper;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using Xrm.Domain.ModelBase;

namespace Xrm.Infrastructure.DataAccess.Mapping
{
    public static class CrmMapper
    {
        public static TDestination ToSdkEntityObject<TSource, TDestination>(this TSource source)
            where TSource : EntityBase
            where TDestination : Entity
        {
            var modifiedFields = source.ModifiedFields != null
                ? source.ModifiedFields : new List<string>();

            Mapper.CreateMap<int?, OptionSetValue>().ConvertUsing<NullableIntToOptionSetConverter>();
            Mapper.CreateMap<Guid?, EntityReference>().ConvertUsing<NullableGuidToEntityReferenceConverter>();
            //Mapper.CreateMap<decimal?, Money>().ConvertUsing<NullableDecimalToMoneyConverter>();

            Mapper.CreateMap<int, OptionSetValue>().ConvertUsing<IntToOptionSetConverter>();
            Mapper.CreateMap<Guid, EntityReference>().ConvertUsing<GuidToEntityReferenceConverter>();
            Mapper.CreateMap<decimal, Money>().ConvertUsing<DecimalToMoneyConverter>();
            Mapper.CreateMap<TSource, TDestination>();

            Mapper.CreateMap<TSource, TDestination>()
                  .ForAllMembers(
                      opt =>
                      opt.Condition(
                              srs =>
                                  (srs.PropertyMap.SourceMember.Name == "Id" && (Guid)srs.SourceValue != Guid.Empty)
                                  || ((srs.PropertyMap.SourceMember.Name != "Id" && srs.SourceValue != null)
                                       && (modifiedFields.Count() > 0 && modifiedFields.Contains(srs.PropertyMap.SourceMember.Name)))
                                       )

                );

            return Mapper.Map<TSource, TDestination>(source);
        }



        public static Guid? EntityReferenceToNullableGuid(EntityReference entityRef)
        {
            return (entityRef != null ? (Guid?)entityRef.Id : null);
        }

        public static int? OptionSetValueToNullableInt(OptionSetValue optionSet)
        {
            return (optionSet != null ? (int?)optionSet.Value : null);
        }

        public class GuidToEntityReferenceConverter : ITypeConverter<Guid, EntityReference>
        {
            public EntityReference Convert(ResolutionContext context)
            {
                return new EntityReference { Id = (Guid)context.SourceValue };
            }
        }

        public class IntToOptionSetConverter : ITypeConverter<int, OptionSetValue>
        {
            public OptionSetValue Convert(ResolutionContext context)
            {
                return new OptionSetValue { Value = (int)context.SourceValue };
            }
        }

        public class NullableGuidToEntityReferenceConverter : ITypeConverter<Guid?, EntityReference>
        {
            public EntityReference Convert(ResolutionContext context)
            {
                var source = (Guid?)context.SourceValue;

                return source.HasValue ? new EntityReference { Id = source.Value } : null;
            }
        }

        public class NullableIntToOptionSetConverter : ITypeConverter<int?, OptionSetValue>
        {
            public OptionSetValue Convert(ResolutionContext context)
            {
                var source = (int?)context.SourceValue;

                return source.HasValue ? new OptionSetValue { Value = source.Value } : null;
            }
        }

        public class DecimalToMoneyConverter : ITypeConverter<decimal, Money>
        {
            public Money Convert(ResolutionContext context)
            {
                var source = (decimal)context.SourceValue;

                return new Money { Value = source };
            }
        }
    }
}
