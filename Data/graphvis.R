# install.packages("plotly")

library(igraph)

# -----------------------------------
# get files
path <- ''

nodes <- read.csv(paste(path, 'nodes.csv', sep = ''))
edges <- read.csv(paste(path, 'edges.csv', sep = ''))

# table(nodes$type)

# -----------------------------------
# constuct graph
g <- graph.data.frame(edges, directed = TRUE, vertices = nodes)

plot(g)


# -----------------------------------
# layout 2d
# -----------------------------------

l.fr <- layout_with_fr(g, dim = 2, niter = 2000)
# l.kk <- layout_with_kk(g, dim = 2)

plot(g, layoyt = l.fr)
# plot(g, layoyt = l.kk)

# l.df <- data.frame(l.fr)
# names(l.df)
# names(nodes)
# l.df$node = nodes$node
# l.df$type = nodes$type


nodes.layout <- cbind(nodes, l.fr)

names(nodes.layout)[5:6] <- c("Z", "Y")

# -----------------------------------
# centre the graph
nodes.layout$Layer <- nodes.layout$Layer - round(mean(nodes.layout$Layer), 0)
nodes.layout$Y <- nodes.layout$Y - mean(nodes.layout$Y)
nodes.layout$Z <- nodes.layout$Z - mean(nodes.layout$Z)

# -----------------------------------
# write csv files
write.csv(nodes.layout, "nodes_layout.csv", row.names = FALSE, quote=FALSE, col.names = FALSE)


