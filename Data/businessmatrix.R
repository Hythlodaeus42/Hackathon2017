# install.packages("dplyr")
library(dplyr)
library(plyr)

# --------------------------
# load data
path <- ''

busmat <- read.csv(paste(path, 'BusinessArchitectureMatrix.csv', sep = ''), sep="|", stringsAsFactors=F, header=F)
# bfg <- read.csv(paste(path, 'BusinessFunctionGroup.csv', sep = ''), sep="|", stringsAsFactors=F, header=F)
bf <- read.csv(paste(path, 'BusinessFunction.csv', sep = ''), sep="|", stringsAsFactors=F, header=F)
ac <- read.csv(paste(path, 'AssetClass.csv', sep = ''), sep="|", stringsAsFactors=F, header=F)

names(busmat) <- c('bfg', 'bf', 'ac', 'app')
# names(bfg) <- c('bfg.ord', 'bfg')
names(bf) <- c('bf.ord', 'bf')
names(ac) <- c('ac.ord', 'ac')

# --------------------------
# add ordinals
busmat.all <- busmat
# busmat.all <- merge(busmat.all, bfg, by = 'bfg')
busmat.all <- merge(busmat.all, bf, by = 'bf')
busmat.all <- merge(busmat.all, ac, by = 'ac')

# --------------------------
# count multiples at intersections
busmat.count <- aggregate(app ~ bf + ac, data = busmat.all, FUN = length)
busmat.all <- merge(busmat.all, busmat.count, by = c('bf', 'ac'))
names(busmat.all)[c(4, 7)] <- c('app', 'count')

busmat.rank <- busmat.all %>% 
  arrange(bf, ac, app) %>%
  group_by(bf, ac) %>%
  mutate(rank=row_number())

# --------------------------
# write files
write.table(busmat.rank, "BusinessArchitectureMatrix.csv", sep = ",", row.names = FALSE, quote=FALSE, col.names = FALSE)
