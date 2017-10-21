# install.packages("dplyr")
library(dplyr)
# library(plyr)

# --------------------------
# load data
path <- ''

busmat <- read.csv(paste(path, 'BusinessArchitectureMatrix.csv', sep = ''), sep="|", stringsAsFactors=F, header=F)
# bfg <- read.csv(paste(path, 'BusinessFunctionGroup.csv', sep = ''), sep="|", stringsAsFactors=F, header=F)
bf <- read.csv(paste(path, 'BusinessFunction.csv', sep = ''), sep="|", stringsAsFactors=F, header=F)
ac <- read.csv(paste(path, 'AssetClass.csv', sep = ''), sep="|", stringsAsFactors=F, header=F)

names(busmat) <- c('bfg', 'bf', 'ac', 'app', 'year', 'colour')
# names(bfg) <- c('bfg', 'bfg.ord')
names(bf) <- c('bf', 'bf.ord')
names(ac) <- c('ac', 'ac.ord')

# --------------------------
# add ordinals
busmat.all <- busmat
# busmat.all <- merge(busmat.all, bfg, by = 'bfg')
busmat.all <- merge(busmat.all, bf, by = 'bf')
busmat.all <- merge(busmat.all, ac, by = 'ac')

# --------------------------
# count multiples at intersections
busmat.count <- aggregate(app ~ bf + ac + year, data = busmat.all, FUN = length)
busmat.all <- merge(busmat.all, busmat.count, by = c('bf', 'ac', 'year'))
names(busmat.all)[c(5, 9)] <- c('app', 'count')

busmat.rank <- busmat.all %>% 
  arrange(bf, ac, year, app) %>%
  group_by(bf, ac, year) %>%
  mutate(rank=row_number())

# --------------------------
# find contiguous apps
bf.ord.min <- min(bf$bf.ord)
bf.ord.max <- max(bf$bf.ord)
ac.ord.min <- min(ac$ac.ord)
ac.ord.max <- max(ac$ac.ord)
years <- unique (busmat.rank$year)


getapp.count <- function(y, b, a) {
  max(busmat.rank[busmat.rank$year == y & busmat.rank$bf.ord == b & busmat.rank$ac.ord == a, ]$count)
}

getapp.name <- function(y, b, a) {
  app.name <- busmat.rank[busmat.rank$year == y & busmat.rank$bf.ord == b & busmat.rank$ac.ord == a, ]$app
  if (length(app.name) == 0) {
    app.name <- "###"
  }
  
  app.name
}


# getapp.count(2018, 5, 5)
# getapp.name(2018, 5, 5)
# 
# busmat.rank[busmat.rank$year == 2018 & busmat.rank$bf.ord == 5 & busmat.rank$ac.ord == 5, ]$app


busmat.rank$leftmatch <- FALSE
busmat.rank$contiguous <- 1

for (y in years) {
  for (b in bf$bf.ord){
    for (a in ac$ac.ord){
      app.count <- getapp.count(y, b, a)
      
      if (app.count == 1) {
        # get current app
        app <- getapp.name(y, b, a)

        
        
        # check left
        if (a > 0) {
          # y <- 2018
          # b <- 5
          # a <- 5
          # print (c(y, b, a))
          
          # leftmatch[[y]][b + 1, a + 1] <- (max(app == getapp.name(y, b, a - 1)) & getapp.count(y, b, a - 1) == 1)
          busmat.rank[busmat.rank$year == y & busmat.rank$bf.ord == b & busmat.rank$ac.ord == a, ]$leftmatch <- (max(app == getapp.name(y, b, a - 1)) & getapp.count(y, b, a - 1) == 1)
        }
        
        n <- 1
        contiguous <- 1
        while (max(getapp.name(y, b, a)) == max(getapp.name(y, b, a + n))) {
          # contiguous[[y]][b + 1, a + 1] <- contiguous[[y]][b + 1, a + 1] + 1
          contiguous <- contiguous + 1
          
          n <- n + 1
        }
        busmat.rank[busmat.rank$year == y & busmat.rank$bf.ord == b & busmat.rank$ac.ord == a, ]$contiguous <- contiguous
      }
    }
  }
}

# bfg04	bf09	ac01	app11	2018	Green
# busmat.rank[busmat.rank$year == 2018 & busmat.rank$bf == "bf09", ]


# View(busmat.rank[busmat.rank$year == y & busmat.rank$bf.ord == b & busmat.rank$ac.ord %in% (a-1):(a + 1), ])


# --------------------------
# write files
write.table(busmat.rank, "BusinessArchitectureMatrix.csv", sep = "|", row.names = FALSE, quote=FALSE, col.names = FALSE)
