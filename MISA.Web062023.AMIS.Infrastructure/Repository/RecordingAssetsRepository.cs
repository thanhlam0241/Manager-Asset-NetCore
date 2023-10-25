using Dapper;
using MISA.Web062023.AMIS.Application;
using MISA.Web062023.AMIS.Domain;

namespace MISA.Web062023.AMIS.Infrastructure
{

    /// <summary>
    /// The recording assets repository.
    /// </summary>
    /// Created by: NTLam (19/8/2023)
    public class RecordingAssetsRepository : IRecordingAssetsRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRecordingRepository _recordingRepository;
        private readonly IResourceAssetRepository _resourceAssetRepository;
        private readonly IRecordedAssetRepository _recordedAssetRepository;
        private readonly IFixedAssetRepository _fixedAssetRepository;

        /// <summary>
        /// The constructor
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="recordingRepository">The recording repository.</param>
        /// Created by: NTLam (19/8/2023)
        public RecordingAssetsRepository(IUnitOfWork unitOfWork, IRecordingRepository recordingRepository, IResourceAssetRepository resourceAssetRepository, IRecordedAssetRepository recordedAssetRepository, IFixedAssetRepository fixedAssetRepository)
        {
            _unitOfWork = unitOfWork;
            _recordingRepository = recordingRepository;
            _resourceAssetRepository = resourceAssetRepository;
            _recordedAssetRepository = recordedAssetRepository;
            _fixedAssetRepository = fixedAssetRepository;
        }

        /// <summary>
        /// The delete recording assets async.
        /// </summary>
        /// <param name="recordingId">The recording id.</param>
        /// <param name="assetIds">The asset ids.</param>
        /// <returns>The result.</returns>
        /// Created by: NTLam (19/8/2023)
        public Task<int> DeleteRecordingAssetsAsync(Guid recordingId, List<Guid> assetIds)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The get recording assets.
        /// </summary>
        /// <param name="recordingId">The recording id.</param>
        /// <returns>The result.</returns>
        /// Created by: NTLam (19/8/2023)
        public async Task<List<RecordedAsset>> GetRecordingAssetsAsync(Guid recordingId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("RecordingId", recordingId);
            var sqlQueryAssets = """
                SELECT 
                recorded_asset.recorded_asset_id as recorded_asset_id, recorded_asset_code, recorded_asset_name,department_name, recorded_asset.value, depreciation_rate, 
                resource_asset.resource_asset_id, resource_asset.cost,
                resource_budget.resource_budget_id, resource_budget_code, resource_budget_name,
                recording.recording_id, recording_code, recording_date, action_date, recording.value, description, recording_type
                FROM recorded_asset
                LEFT JOIN resource_asset ON recorded_asset.recorded_asset_id = resource_asset.recorded_asset_id
                LEFT JOIN resource_budget ON resource_asset.resource_budget_id = resource_budget.resource_budget_id
                LEFT JOIN recording ON recorded_asset.recording_id = recording.recording_id
                WHERE recorded_asset.recording_id = @RecordingId
                """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;
            var listAssets1 = await _unitOfWork.Connection.QueryAsync(sqlQueryAssets, parameters);
            DefaultTypeMap.MatchNamesWithUnderscores = true;
            var listAssets = await _unitOfWork.Connection.QueryAsync<RecordedAsset, ResourceAsset, ResourceBudget, Recording, RecordedAsset>(sqlQueryAssets, (asset, resourceAsset, resourceBudget, recording) =>
            {
                if (resourceAsset != null && resourceBudget != null)
                {
                    resourceAsset.ResourceBudget = resourceBudget;
                    asset.ResourceAssets.Add(resourceAsset);
                }
                asset.Recording = recording;
                return asset;
            }
                , parameters, splitOn: "resource_asset_id, resource_budget_id, recording_id");

            var result = listAssets.GroupBy(a => a.RecordedAssetId).Select(
                    a =>
                    {
                        var asset = a.First();
                        asset.ResourceAssets = a.Where(x => x.ResourceAssets.Count > 0).Select(ra => ra.ResourceAssets.First()).ToList();
                        return asset;
                    }
                );
            return result.ToList();

        }

        /// <summary>
        /// The insert recording assets.
        /// </summary>
        /// <param name="recordingAssets">The recording assets.</param>
        /// <returns>The result.</returns>
        /// Created by: NTLam (19/8/2023)
        public async Task<int> InsertRecordingAssetsAsync(RecordingAsset recordingAssets)
        {
            var recording = recordingAssets.Recording;
            var assets = recordingAssets.Assets;

            List<dynamic> paramInsertRecordedAssets = new();
            foreach (var asset in assets)
            {
                paramInsertRecordedAssets.Add(new
                {
                    asset.RecordedAssetId,
                    asset.RecordedAssetCode,
                    asset.RecordedAssetName,
                    asset.DepartmentName,
                    asset.Value,
                    recording.RecordingId,
                    asset.DepreciationRate
                });
            }

            var sqlInsertRecording = """
                INSERT INTO recording (recording_id,recording_code,recording_date,action_date,value,description,recording_type) 
                    VALUES (@RecordingId, @RecordingCode, @RecordingDate, @ActionDate,@Value, @Description,1)
                """;

            var parametersInsertRecording = new DynamicParameters();
            parametersInsertRecording.Add("RecordingId", recording.RecordingId);
            parametersInsertRecording.Add("RecordingCode", recording.RecordingCode);
            parametersInsertRecording.Add("RecordingDate", recording.RecordingDate);
            parametersInsertRecording.Add("ActionDate", recording.ActionDate);
            parametersInsertRecording.Add("Description", recording.Description);
            parametersInsertRecording.Add("Value", recording.Value);

            List<dynamic> resourceAssets = new List<dynamic>();
            foreach (RecordedAsset asset in assets)
            {
                foreach (ResourceAsset ra in asset.ResourceAssets)
                {
                    if (ra.ResourceBudget != null)
                    {
                        resourceAssets.Add(new
                        {
                            ResourceBudget = ra.ResourceBudget.ResourceBudgetId,
                            AssetId = asset.RecordedAssetId,
                            ra.Cost
                        });
                    }
                }
            }

            var sqlInsertRecordedAsset = $"""
                INSERT INTO recorded_asset (recorded_asset_id, recorded_asset_code, recorded_asset_name, recording_id, department_name, value, depreciation_rate) 
                    VALUES (@RecordedAssetId, @RecordedAssetCode, @RecordedAssetName, @RecordingId ,@DepartmentName, @Value, @DepreciationRate)
                """;

            var sqlInsertResourceAsset = """
                                         INSERT INTO resource_asset (resource_budget_id, recorded_asset_id, cost) 
                                         VALUES (@ResourceBudget, @AssetId, @Cost)
                                         """;

            var connection = _unitOfWork.Connection;
            connection.Open();
            var transaction = connection.BeginTransaction();
            try
            {

                connection.Execute(sqlInsertRecording, parametersInsertRecording, transaction);
                connection.Execute(sqlInsertRecordedAsset, paramInsertRecordedAssets, transaction);
                connection.Execute(sqlInsertResourceAsset, resourceAssets, transaction);
                transaction.Commit();
                connection.Close();
                return 1;
            }
            catch (Exception e)
            {
                transaction.Rollback();
                connection.Close();
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// The create list asset.
        /// </summary>
        /// <param name="recordingId">The recording id.</param>
        /// <param name="recordedAssets">The recorded assets.</param>
        /// <returns>The result.</returns>
        public async Task<bool> CreateListAsset(Guid recordingId, List<RecordedAsset> recordedAssets)
        {
            var sqlInsertRecordedAsset = """
                INSERT INTO recorded_asset (recorded_asset_id, recorded_asset_code, recorded_asset_name, recording_id, department_name, value, depreciation_rate) 
                VALUES (@RecordedAssetId, @RecordedAssetCode, @RecordedAssetName, @RecordingId ,@DepartmentName, @Value, @DepreciationRate)
                """;
            var sqlInsertResourceAsset = """
                INSERT INTO resource_asset (resource_budget_id, recorded_asset_id, cost)
                VALUES (@ResourceBudget, @AssetId, @Cost)
                """;

            var listFixedAsset = await _fixedAssetRepository.GetListEntitiesByListCode(recordedAssets.Select(x => x.RecordedAssetCode).ToList());

            var paramsInsertRecordedAsset = new List<dynamic>();
            var paramsInsertResourceAsset = new List<dynamic>();
            foreach (var asset in recordedAssets)
            {
                var fixedAsset = listFixedAsset.Where(x => x.FixedAssetCode == asset.RecordedAssetCode).FirstOrDefault();
                Guid newId = Guid.NewGuid();
                if (asset.ResourceAssets == null || asset.ResourceAssets.Count == 0)
                {
                    paramsInsertRecordedAsset.Add(new
                    {
                        RecordedAssetId = newId,
                        asset.RecordedAssetCode,
                        RecordedAssetName = fixedAsset.FixedAssetName,
                        fixedAsset.DepartmentName,
                        Value = (int)fixedAsset.Cost,
                        RecordingId = recordingId,
                        fixedAsset.DepreciationRate
                    });
                }
                else
                {
                    foreach (var ra in asset.ResourceAssets)
                    {
                        if (ra.ResourceBudget == null)
                        {
                            throw new BadRequestException("Nguồn tài sản không được để trống");
                        }
                        paramsInsertResourceAsset.Add(new
                        {
                            ResourceBudget = ra.ResourceBudget.ResourceBudgetId,
                            AssetId = newId,
                            ra.Cost
                        });
                    }
                    if (asset.Value != asset.ResourceAssets.Sum(ra => ra.Cost))
                    {
                        throw new BadRequestException("Giá trị tài sản không khớp với tổng giá trị các nguồn tài sản");
                    }
                    paramsInsertRecordedAsset.Add(new
                    {
                        RecordedAssetId = newId,
                        asset.RecordedAssetCode,
                        RecordedAssetName = fixedAsset.FixedAssetName,
                        fixedAsset.DepartmentName,
                        Value = (int)asset.Value,
                        RecordingId = recordingId,
                        fixedAsset.DepreciationRate
                    });
                }
            }
            var resultInsertRecorded = await _unitOfWork.Connection.ExecuteAsync(sqlInsertRecordedAsset, paramsInsertRecordedAsset, _unitOfWork.Transaction);
            if (resultInsertRecorded > 0)
            {
                if (paramsInsertResourceAsset.Count > 0)
                {
                    var resultInsertResource = await _unitOfWork.Connection.ExecuteAsync(sqlInsertResourceAsset, paramsInsertResourceAsset, _unitOfWork.Transaction);
                    if (resultInsertResource > 0)
                    {
                        return true;
                    }
                }
                else
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// The delete asset by params.
        /// </summary>
        /// <param name="lists">The lists.</param>
        /// <returns>The result.</returns>
        public async Task<bool> DeleteAssetByParams(List<Guid?> lists)
        {
            var sqlDeleteRecordedAsset = """
                DELETE FROM recorded_asset
                WHERE recorded_asset_id IN @RecordedAssetId
                """;
            if (lists.Count > 0)
            {
                var result = await _unitOfWork.Connection.ExecuteAsync(sqlDeleteRecordedAsset, new { RecordedAssetId = lists }, _unitOfWork.Transaction);
                return result > 0;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// The delete resource.
        /// </summary>
        /// <param name="lists">The lists.</param>
        /// <returns>The result.</returns>
        public async Task<bool> DeleteResource(List<int> lists)
        {
            var sqlDeleteResourceAsset = """
                DELETE FROM resource_asset
                WHERE resource_asset_id IN @ResourceAssetId
                """;
            if (lists.Count > 0)
            {
                var result = await _unitOfWork.Connection.ExecuteAsync(sqlDeleteResourceAsset, new { ResourceAssetId = lists }, _unitOfWork.Transaction);
                return result > 0;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// The update recording assets async.
        /// </summary>
        /// <param name="recording">The recording.</param>
        /// <param name="oldAssets">The old assets.</param>
        /// <param name="newAssets">The new assets.</param>
        /// <returns>The result.</returns>
        /// Created by: NTLam (19/8/2023)
        public async Task<bool> UpdateRecordingAssetsAsync(Recording recording, List<RecordedAsset> oldAssets, List<RecordedAsset> newAssets)
        {
            // Danh sách mã tài sản cũ
            var listCodeOldAssets = oldAssets.Select(a => a.RecordedAssetCode).ToList();
            // Danh sách mã tài sản được cập nhật
            var listCodeUpdateAssets = newAssets.Where(a => a.RecordedAssetId != null && a.RecordedAssetCode != null).Select(a => a.RecordedAssetCode).ToList();
            // Danh sách Id tài sản bị xóa
            var listIdDelete = newAssets.Where(a => a.RecordedAssetCode == null).Select(a => a.RecordedAssetId).ToList();
            // Danh sách tài sản được tạo mới
            var listAssetCreate = newAssets.Where(a => a.RecordedAssetId == null).ToList();

            // Danh sách các tham số cập nhật tài sản (nếu có)
            var paramsUpdateRecordedAsset = new List<dynamic>();
            // Danh sách các tham số cập nhật các nguồn hình thành của các tài sản (nếu có)
            var paramsUpdateResourceAsset = new List<dynamic>();
            // Danh sách tham số các id nguồn hình thành bị xóa (nếu có)
            var paramsDeleteResourceAsset = new List<int>();
            // Nguyên giá tổng mới của chứng từ
            var newValueRecording = oldAssets.Where(a => !newAssets.Select(a => a.RecordedAssetCode).Contains(a.RecordedAssetCode)).Sum(a => a.Value) + newAssets.Sum(a => a.Value);
            // Tham số cập nhật chứng từ
            var paramsUpdateRecording = new
            {
                recording.RecordingId,
                recording.RecordingCode,
                recording.RecordingDate,
                recording.ActionDate,
                Value = newValueRecording,
                recording.Description
            };
            // Duyệt qua danh sách mã tài sản được cập nhật
            foreach (var code in listCodeUpdateAssets)
            {
                var oldAsset = oldAssets.Where(a => a.RecordedAssetCode == code).FirstOrDefault();
                var newAsset = newAssets.Where(a => a.RecordedAssetCode == code).FirstOrDefault();

                if (oldAsset != null && newAsset != null)
                {
                    if (oldAsset.ResourceAssets.Any())
                    {
                        var listIdOldResourceAsset = oldAsset.ResourceAssets.Select(ra => ra.ResourceAssetId).ToList();
                        var listIdNewResourceAsset = newAsset.ResourceAssets.Select(ra => ra.ResourceAssetId).ToList();
                        var listIdDeleteResourceAsset = listIdOldResourceAsset.Except(listIdNewResourceAsset).ToList();
                        foreach (var id in listIdDeleteResourceAsset)
                        {
                            if (id != null)
                            {
                                paramsDeleteResourceAsset.Add(id.Value);
                            }
                        }
                    }
                    if (newAsset.ResourceAssets == null || newAsset.ResourceAssets.Count == 0)
                    {
                        var fixedAsset = await _fixedAssetRepository.FindByCodeAsync(code)
                            ?? throw new NotFoundException($"Không tìm thấy tài sản mã {code}");
                        if (oldAsset.Value != fixedAsset.Cost)
                        {
                            paramsUpdateRecordedAsset.Add(new
                            {
                                oldAsset.RecordedAssetId,
                                Value = fixedAsset.Cost
                            });
                        }
                    }
                    else
                    {
                        foreach (var ra in newAsset.ResourceAssets)
                        {
                            var resourceId = ra.ResourceAssetId;
                            if (resourceId == null)
                            {
                                paramsUpdateResourceAsset.Add(new
                                {
                                    ra.ResourceAssetId,
                                    ResourceBudget = ra.ResourceBudget.ResourceBudgetId,
                                    AssetId = newAsset.RecordedAssetId,
                                    ra.Cost
                                });
                            }
                            else
                            {
                                var oldResourceAsset = oldAsset.ResourceAssets.Where(ra => ra.ResourceAssetId == resourceId).FirstOrDefault();
                                bool isUpdate = oldResourceAsset.Cost != ra.Cost || oldResourceAsset.ResourceBudget.ResourceBudgetId != ra.ResourceBudget.ResourceBudgetId;
                                if (isUpdate)
                                {
                                    paramsUpdateResourceAsset.Add(new
                                    {
                                        ra.ResourceAssetId,
                                        ResourceBudget = ra.ResourceBudget.ResourceBudgetId,
                                        AssetId = newAsset.RecordedAssetId,
                                        ra.Cost
                                    });
                                }
                            }
                        }
                        if (newAsset.Value != newAsset.ResourceAssets.Sum(ra => ra.Cost))
                        {
                            throw new BadRequestException("Giá trị tài sản không khớp với tổng giá trị các nguồn tài sản");
                        }
                        if (oldAsset.Value != newAsset.Value)
                        {
                            paramsUpdateRecordedAsset.Add(new
                            {
                                newAsset.RecordedAssetId,
                                newAsset.Value
                            });
                        }
                    }
                }
                else
                {
                    throw new ConflictException("Error");
                }
            }
            // Danh sách các tài sản cố định được tạo mới trong chứng từ
            var listFixedAsset = await _fixedAssetRepository.GetListEntitiesByListCode(listAssetCreate.Select(x => x.RecordedAssetCode).ToList());
            // Danh sách tham số tài sản được chứng từ
            var paramsInsertRecordedAsset = new List<dynamic>();
            // Danh sách tham số nguồn hình thành của các tài sản được chứng từ
            var paramsInsertResourceAsset = new List<dynamic>();
            // Duyệt qua danh sách tài sản được tạo mới
            foreach (var asset in listAssetCreate)
            {
                var fixedAsset = listFixedAsset.Where(x => x.FixedAssetCode == asset.RecordedAssetCode).FirstOrDefault();
                Guid newId = Guid.NewGuid();
                if (asset.ResourceAssets == null || asset.ResourceAssets.Count == 0)
                {
                    paramsInsertRecordedAsset.Add(new
                    {
                        RecordedAssetId = newId,
                        asset.RecordedAssetCode,
                        RecordedAssetName = fixedAsset.FixedAssetName,
                        fixedAsset.DepartmentName,
                        Value = (int)fixedAsset.Cost,
                        RecordingId = recording.RecordingId,
                        fixedAsset.DepreciationRate
                    });
                }
                else
                {
                    foreach (var ra in asset.ResourceAssets)
                    {
                        if (ra.ResourceBudget == null)
                        {
                            throw new BadRequestException("Nguồn tài sản không được để trống");
                        }
                        paramsInsertResourceAsset.Add(new
                        {
                            ResourceBudget = ra.ResourceBudget.ResourceBudgetId,
                            AssetId = newId,
                            ra.Cost
                        });
                    }
                    if (asset.Value != asset.ResourceAssets.Sum(ra => ra.Cost))
                    {
                        throw new BadRequestException("Giá trị tài sản không khớp với tổng giá trị các nguồn tài sản");
                    }
                    paramsInsertRecordedAsset.Add(new
                    {
                        RecordedAssetId = newId,
                        asset.RecordedAssetCode,
                        RecordedAssetName = fixedAsset.FixedAssetName,
                        fixedAsset.DepartmentName,
                        Value = (int)asset.Value,
                        RecordingId = recording.RecordingId,
                        fixedAsset.DepreciationRate
                    });
                }
            }
            // Các câu lệnh sql
            // Cập nhật nguồn tài sản
            var sqlUpdateResourceAsset = """
                INSERT INTO resource_asset (resource_asset_id, resource_budget_id, recorded_asset_id, cost, modified_date)
                VALUES (@ResourceAssetId, @ResourceBudget, @AssetId, @Cost, NOW())
                ON DUPLICATE KEY UPDATE cost = @Cost, resource_budget_id = @ResourceBudget
                """;
            // Cập nhật tài sản chứng từ
            var sqlUpdateRecordedAsset = """
                UPDATE recorded_asset SET value = @Value, modified_date = NOW()
                WHERE recorded_asset_id = @RecordedAssetId
                """;
            // Cập nhật chứng từ
            var sqlUpdateRecording = """
                UPDATE recording
                SET recording_code = @RecordingCode, recording_date = @RecordingDate, action_date = @ActionDate, value = @Value, description = @Description, modified_date = NOW()
                WHERE recording_id = @RecordingId
                """;
            // Xóa tài sản chứng từ
            var sqlDeleteRecordedAsset = """
                DELETE FROM recorded_asset
                WHERE recorded_asset_id IN @RecordedAssetId
                """;
            // Xóa nguồn tài sản
            var sqlDeleteResourceAsset = """
                DELETE FROM resource_asset
                WHERE resource_asset_id IN @ResourceAssetId
                """;
            // Thêm các tài sản chứng từ
            var sqlInsertRecordedAsset = """
                INSERT INTO recorded_asset (recorded_asset_id, recorded_asset_code, recorded_asset_name, recording_id, department_name, value, depreciation_rate) 
                VALUES (@RecordedAssetId, @RecordedAssetCode, @RecordedAssetName, @RecordingId ,@DepartmentName, @Value, @DepreciationRate)
                """;
            // Thêm các nguồn tài sản
            var sqlInsertResourceAsset = """
                INSERT INTO resource_asset (resource_budget_id, recorded_asset_id, cost)
                VALUES (@ResourceBudget, @AssetId, @Cost)
                """;

            var connection = _unitOfWork.Connection;
            connection.Open();
            var transaction = connection.BeginTransaction();
            try
            {
                connection.Execute(sqlUpdateRecording, paramsUpdateRecording, transaction);
                connection.Execute(sqlUpdateRecordedAsset, paramsUpdateRecordedAsset, transaction);
                connection.Execute(sqlUpdateResourceAsset, paramsUpdateResourceAsset, transaction);
                connection.Execute(sqlDeleteRecordedAsset, new { RecordedAssetId = listIdDelete }, transaction);
                connection.Execute(sqlDeleteResourceAsset, new { ResourceAssetId = paramsDeleteResourceAsset }, transaction);
                connection.Execute(sqlInsertRecordedAsset, paramsInsertRecordedAsset, transaction);
                connection.Execute(sqlInsertResourceAsset, paramsInsertResourceAsset, transaction);
                transaction.Commit();
                connection.Close();
                return true;
            }
            catch (Exception e)
            {
                transaction.Rollback();
                connection.Close();
                throw new Exception(e.Message);
            }

            return false;
        }
    }
}
