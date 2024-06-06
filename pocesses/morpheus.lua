local bint = require('.bint')(256)
local ao = require('ao')
local json = require('json')

Handlers.add('talk', Handlers.utils.hasMatchingTag('Action', 'talk'),
  function(msg) 
    
    ao.send({ Target = msg.From, Data = "Hi " .. msg.Tags.Name .. ", are you The One? Can you create a token for me?"}) 
end)