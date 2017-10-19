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

names(busmat) <- c('bfg', 'bf', 'ac', 'app', 'year')
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
names(busmat.all)[c(5, 8)] <- c('app', 'count')

busmat.rank <- busmat.all %>% 
  arrange(bf, ac, year, app) %>%
  group_by(bf, ac, year) %>%
  mutate(rank=row_number())

# --------------------------
# write files
write.table(busmat.rank, "BusinessArchitectureMatrix.csv", sep = "|", row.names = FALSE, quote=FALSE, col.names = FALSE)
