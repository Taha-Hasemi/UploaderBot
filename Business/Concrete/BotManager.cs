﻿using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Core.Aspects.Autofac.Caching;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete;

public class BotManager : IBotService
{
    IBotDal _botDal;

    public BotManager(IBotDal botDal)
    {
        _botDal = botDal;
    }

    [SecuredOperation("admin")]
    public List<Bot> GetActiveBots()
    {
        return _botDal.GetList(x => x.Active && x.Status).ToList();
    }

    [CacheAspect(86400)]
    public string GetTokenByChatId(long chatId)
    {
        return _botDal.Get(x => x.BotChatId == chatId && x.Status).Token;
    }
}
