﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;
using SlickOne.WebUtility;
using Slickflow.Data;
using Slickflow.Engine.Common;
using Slickflow.Engine.Service;
using Slickflow.BizAppService.Entity;
using Slickflow.BizAppService.Interface;
using Slickflow.BizAppService.Service;

namespace SfDemo.WebApi.Controllers
{
    /// <summary>
    /// 生产订单控制器
    /// 示例代码，请勿直接作为生产项目代码使用。
    /// </summary>
    public partial class ProductOrderController : ApiControllerBase
    {
        #region 基本数据操作
        private IProductOrderService _service;
        protected IProductOrderService ProductOrderService
        {
            get
            {
                if (_service == null)
                {
                    _service = new ProductOrderService();
                }
                return _service;
            }
        }

        /// <summary>
        /// 获取生产订单数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ResponseResult<ProductOrderEntity> Get(int id)
        {
            return Get<ProductOrderEntity>(id);
        }

        /// <summary>
        /// 插入ProductOrder数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseResult<dynamic> Insert(ProductOrderEntity entity)
        {
            return Insert<ProductOrderEntity>(entity);
        }

        /// <summary>
        /// 更新ProductOrder数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPut]
        public ResponseResult Update(ProductOrderEntity entity)
        {
            return Update<ProductOrderEntity>(entity);
        }

        /// <summary>
        /// 删除ProductOrder数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public ResponseResult Delete(int id)
        {
            return Delete<ProductOrderEntity>(id);
        }
        #endregion

        /// <summary>
        /// 数据分页显示
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseResult<List<ProductOrderEntity>> QueryPaged(ProductOrderQuery query)
        {
            var result = ResponseResult<List<ProductOrderEntity>>.Default();
            var conn = SessionFactory.CreateConnection();
            try
            {
                var count = 0;
                var list = ProductOrderService.GetPaged(query, out count);
                result = ResponseResult<List<ProductOrderEntity>>.Success(list);
                result.TotalRowsCount = count;
                result.TotalPages = (count + query.PageSize - 1) / query.PageSize;
            }
            catch (System.Exception ex)
            {
                result = ResponseResult<List<ProductOrderEntity>>.Error(
                    string.Format("获取{0}分页数据失败，错误{1}", "ProductOrder", ex.Message)
                );
            }
            return result;
        }

        /// <summary>
        /// 同步订单
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ResponseResult<ProductOrderEntity> SyncOrder()
        {
            var result = ResponseResult<ProductOrderEntity>.Default();
            try
            {
                var entity = ProductOrderService.SyncOrder();
                result = ResponseResult<ProductOrderEntity>.Success(entity, "同步订单数据成功！");
            }
            catch (System.Exception ex)
            {
                result = ResponseResult<ProductOrderEntity>.Error(
                    string.Format("同步订单{0}失败, 错误:{1}", "ProductOrder", ex.Message)
                );
            }
            return result;
        }

        /// <summary>
        /// 检查是否已经被派单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ResponseResult CheckDispatched(int id)
        {
            //检查是否已经进入流程阶段
            var result = ResponseResult.Default();
            var orderEntity = QuickRepository.GetById<ProductOrderEntity>(id);
            if (orderEntity.Status == 1)
            {
                result = ResponseResult.Success();
            }
            else
            {
                result = ResponseResult.Error("订单已经进入流程状态，不能再次派单！");
            }
            return result;
        }

        /// <summary>
        /// 分派订单(业务员)
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseResult Dispatch(dynamic entity)
        {
            var result = ResponseResult.Default();
            try
            {
                var productOrder = JsonConvert.DeserializeObject<ProductOrderEntity>(entity.ProductOrderEntity.ToString());
                var runner = JsonConvert.DeserializeObject<WfAppRunner>(entity.WfAppRunner.ToString());

                IWfServiceRegister s = ProductOrderService as IWfServiceRegister;
                s.RegisterWfAppRunner(runner);

                WfAppResult appResult = ProductOrderService.Dispatch(productOrder);
                if (appResult.Status == 1)
                    result = ResponseResult.Success("分派订单数据成功！");
                else
                    result = ResponseResult.Error(string.Format("分派订单失败:{0}", appResult.Message));
            }
            catch (System.Exception ex)
            {
                result = ResponseResult.Error(
                    string.Format("分派订单{0}失败, 错误:{1}", "ProductOrder", ex.Message)
                );
            }
            return result;
        }

        /// <summary>
        /// 打样(技术员)
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseResult Sample(dynamic entity)
        {
            var result = ResponseResult.Default();
            try
            {
                var productOrder = JsonConvert.DeserializeObject<ProductOrderEntity>(entity.ProductOrderEntity.ToString());
                var runner = JsonConvert.DeserializeObject<WfAppRunner>(entity.WfAppRunner.ToString());
                
                IWfServiceRegister s = ProductOrderService as IWfServiceRegister;
                s.RegisterWfAppRunner(runner);

                WfAppResult appResult = ProductOrderService.Sample(productOrder);
                if (appResult.Status == 1)
                    result = ResponseResult.Success("打样操作成功！");
                else
                    result = ResponseResult.Error(string.Format("打样操作失败:{0}", appResult.Message));
            }
            catch (System.Exception ex)
            {
                result = ResponseResult.Error(
                    string.Format("打样{0}失败, 错误:{1}", "ProductOrder", ex.Message)
                );
            }
            return result;
        }

        /// <summary>
        /// 生产(跟单员)
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseResult Manufacture(dynamic entity)
        {
            var result = ResponseResult.Default();
            try
            {
                var productOrder = JsonConvert.DeserializeObject<ProductOrderEntity>(entity.ProductOrderEntity.ToString());
                var runner = JsonConvert.DeserializeObject<WfAppRunner>(entity.WfAppRunner.ToString());

                IWfServiceRegister s = ProductOrderService as IWfServiceRegister;
                s.RegisterWfAppRunner(runner);

                WfAppResult appResult = ProductOrderService.Manufacture(productOrder);
                if (appResult.Status == 1)
                    result = ResponseResult.Success("生产产品成功！");
                else
                    result = ResponseResult.Error(string.Format("生产产品失败:{0}", appResult.Message));
            }
            catch (System.Exception ex)
            {
                result = ResponseResult.Error(
                    string.Format("生产产品{0}失败, 错误:{1}", "ProductOrder", ex.Message)
                );
            }
            return result;
        }

        /// <summary>
        /// 质检(质检员)
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseResult QCCheck(dynamic entity)
        {
            var result = ResponseResult.Default();
            try
            {
                var productOrder = JsonConvert.DeserializeObject<ProductOrderEntity>(entity.ProductOrderEntity.ToString());
                var runner = JsonConvert.DeserializeObject<WfAppRunner>(entity.WfAppRunner.ToString());

                IWfServiceRegister s = ProductOrderService as IWfServiceRegister;
                s.RegisterWfAppRunner(runner);

                WfAppResult appResult = ProductOrderService.QCCheck(productOrder);
                if (appResult.Status == 1)
                    result = ResponseResult.Success("产品质检成功！");
                else
                    result = ResponseResult.Error(string.Format("产品质检失败:{0}", appResult.Message));
            }
            catch (System.Exception ex)
            {
                result = ResponseResult.Error(
                    string.Format("产品质检{0}失败, 错误:{1}", "ProductOrder", ex.Message)
                );
            }
            return result;
        }

        /// <summary>
        /// 称重(包装员)
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseResult Weight(dynamic entity)
        {
            var result = ResponseResult.Default();
            try
            {
                var productOrder = JsonConvert.DeserializeObject<ProductOrderEntity>(entity.ProductOrderEntity.ToString());
                var runner = JsonConvert.DeserializeObject<WfAppRunner>(entity.WfAppRunner.ToString());

                IWfServiceRegister s = ProductOrderService as IWfServiceRegister;
                s.RegisterWfAppRunner(runner);

                WfAppResult appResult = ProductOrderService.Weight(productOrder);
                if (appResult.Status == 1)
                    result = ResponseResult.Success("称重成功！");
                else
                    result = ResponseResult.Error(string.Format("称重失败:{0}", appResult.Message));
            }
            catch (System.Exception ex)
            {
                result = ResponseResult.Error(
                    string.Format("称重{0}失败, 错误:{1}", "ProductOrder", ex.Message)
                );
            }
            return result;
        }

        /// <summary>
        /// 发货(包装员)
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseResult Delivery(dynamic entity)
        {
            var result = ResponseResult.Default();
            try
            {
                var productOrder = JsonConvert.DeserializeObject<ProductOrderEntity>(entity.ProductOrderEntity.ToString());
                var runner = JsonConvert.DeserializeObject<WfAppRunner>(entity.WfAppRunner.ToString());

                IWfServiceRegister s = ProductOrderService as IWfServiceRegister;
                s.RegisterWfAppRunner(runner);

                WfAppResult appResult = ProductOrderService.Delivery(productOrder);

                if (appResult.Status == 1)
                    result = ResponseResult.Success("发货成功！");
                else
                    result = ResponseResult.Error(string.Format("发货失败:{0}", appResult.Message));
            }
            catch (System.Exception ex)
            {
                result = ResponseResult.Error(
                    string.Format("发货{0}失败, 错误:{1}", "ProductOrder", ex.Message)        
                );
            }
            return result;
        }
    }
}
